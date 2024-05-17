using FluentValidation;
using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using q_wallet.Applications.Behaviour;
using q_wallet.Applications.Responses.Common;
using q_wallet.Domain.Interfaces;
using q_wallet.Infrastructure.Data;
using q_wallet.Infrastructure.Implementations.Repositories;
using System;
using System.Reflection;

namespace q_wallet
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; set; }

		public void ConfigureServices(IServiceCollection services)
		{
			//services.AddCors(options =>
			//{
			//	options.AddPolicy("CORSPolicy", options => options.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed((hosts) => true));
			//});

			// Register AppDbContext
			//var connectionString = Configuration.GetConnectionString("WalletConnection");
			var connectionString = Configuration.GetConnectionString("DefaultConnection");
			services.AddDbContextPool<DataContext>(db => db.UseSqlServer(connectionString));

			//Add services to DI container
			services.AddControllers();
			// services.AddApiVersioning();
			services.AddAutoMapper(typeof(Startup));
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Q Wallet", Description = "Q Wallet Service", Version = "v1" });
			});
			//services.AddHealthChecks();
			services.AddHealthChecks().Services.AddDbContext<DataContext>();

			//Register all middlewares
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
			services.AddTransient(typeof(IApiResponse<>), typeof(ApiResponse<>));
			services.AddTransient(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
			services.AddScoped<IUserAccountRepository, UserAccountRepository>();
			services.AddScoped<IBankAccountRepository, BankAccountRepository>();
			services.AddScoped<IBankAccountTypeRepository, BankAccountTypeRepository>();
		}

		/// <summary>
		/// Configure statrt up processes
		/// </summary>
		/// <param name="app"></param>
		/// <param name="env"></param>
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			//For development only
			app.UseSwagger();
			app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "Q-Wallet v1"));

			//app.UseCors("CORSPolicy");
			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseStaticFiles();
			app.UseAuthorization();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapGet("/", context =>
				{
					context.Response.Redirect("/swagger", permanent: false);
					return Task.CompletedTask;
				});
				endpoints.MapControllers();
				endpoints.MapHealthChecks("/health", new HealthCheckOptions
				{
					Predicate = _ => true,
					ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
				});
			});
		}
	}
}
