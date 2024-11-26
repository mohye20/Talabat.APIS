using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.APIS.Extensions;
using Talabat.APIS.Middlewares;
using Talabat.Core.Entites.Identity;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;

namespace Talabat.APIS
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			#region Configure Services For Add services to the container.

			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddDbContext<StoreContext>(Options =>
			{
				Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});

			builder.Services.AddApplicationServices();

			builder.Services.AddSingleton<IConnectionMultiplexer>(Options =>
			{
				var Connection = builder.Configuration.GetConnectionString("RedisConnectino");
				return ConnectionMultiplexer.Connect(Connection);
			});

			builder.Services.AddDbContext<AppIdentityDbContext>(Options =>
			{
				Options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
			});

			builder.Services.AddIdentityService(builder.Configuration);

			#endregion Configure Services For Add services to the container.

			var app = builder.Build();

			#region Update-Database

			//StoreContext DbContext = new StoreContext(); // Invalid
			//await DbContext.Database.MigrateAsync();

			// Group of Services Life Time Scooped
			using var Scope = app.Services.CreateScope();
			// Services its Self
			var Services = Scope.ServiceProvider;

			var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();
			try
			{
				//Ask CLR For Creating Object From DbContext Explicitly
				var dbContext = Services.GetRequiredService<StoreContext>();
				await dbContext.Database.MigrateAsync();

				var IdentityDbContext = Services.GetRequiredService<AppIdentityDbContext>();
				IdentityDbContext.Database.MigrateAsync();
				var UserManger = Services.GetRequiredService<UserManager<AppUser>>();

				#region Data-Seeding

				await StoreContextSeed.SeedAsync(dbContext);
				await AppIdentityDbContextSeed.SeedUserAsync(UserManger);

				#endregion Data-Seeding
			}
			catch (Exception Ex)
			{
				var Logger = LoggerFactory.CreateLogger<Program>();
				Logger.LogError(Ex, "An Error Occured During Appling Migration");
			}

			#endregion Update-Database

			#region Configure the HTTP request pipeline.

			if (app.Environment.IsDevelopment())
			{
				app.UseMiddleware<ExceptionMiddleware>();
				app.UseSwaggerMiddlewares();
			}



			app.UseStatusCodePagesWithReExecute("/error/{0}");
			app.UseStaticFiles();
			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseAuthorization();
			app.MapControllers();


			#endregion Configure the HTTP request pipeline.

			app.Run();
		}
	}
}