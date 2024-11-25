using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.APIS.Errors;
using Talabat.APIS.Extensions;
using Talabat.APIS.Helpers;
using Talabat.APIS.Middlewares;
using Talabat.Core.Repositories;
using Talabat.Repository;
using Talabat.Repository.Data;

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

				#region Data-Seeding

				await StoreContextSeed.SeedAsync(dbContext);

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

			app.UseStaticFiles();

			app.UseStatusCodePagesWithReExecute("/error/{0}");

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();

			#endregion Configure the HTTP request pipeline.

			app.Run();
		}
	}
}