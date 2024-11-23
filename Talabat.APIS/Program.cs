using Microsoft.EntityFrameworkCore;
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
				var DbContext = Services.GetRequiredService<StoreContext>();
				await DbContext.Database.MigrateAsync();
			}
			catch (Exception Ex)
			{
				var Logger = LoggerFactory.CreateLogger<Program>();
				Logger.LogError(Ex,"An Error Occured During Appling Migration");
			}

			#endregion Update-Database

			#region Configure the HTTP request pipeline.

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();

			#endregion Configure the HTTP request pipeline.

			app.Run();
		}
	}
}