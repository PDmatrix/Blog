using Blog.API.Application.Interfaces;
using Blog.API.Application.Posts.Commands;
using Blog.API.Application.Posts.Models;
using Blog.API.Application.Posts.Queries;
using Blog.API.Infrastructure;
using Blog.API.MarkDig;
using Blog.API.Persistence;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Blog.API
{
	public class Startup
	{
		private IConfiguration Config { get; }
		private IHostingEnvironment Environment { get; }
		
		public Startup(IConfiguration config, IHostingEnvironment environment)
		{
			Config = config;
			Environment = environment;
		}
	
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<AuthSettings>(Config.GetSection("AuthOptions"));
			services.AddOptions();
			services.AddCustomApiVersioning();
			services.AddCustomMvc(Environment);
			var authSettings = Config.GetSection("AuthOptions").Get<AuthSettings>();
			services.AddCustomAuthentication(authSettings.Authority, authSettings.Scope);
			services.AddCustomHttpClientFactory(authSettings.Authority);
			services.AddCustomSwagger();
			services.AddScoped<IUnitOfWorkFactory>(provider => 
					new UnitOfWorkFactory(Config.GetConnectionString("DefaultConnection")));
			services.AddScoped<IConverter<string, string>, MarkdownConverter>();
			services.AddMediatR(typeof(GetAllPostsHandler));
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			
			if (!env.IsEnvironment("Testing"))
				app.UseAuthentication();
			
			app.UseMvc(routes =>
			{
				routes.MapRoute(
					"DefaultApi",
					"api/{controller}/{action}");
			});
			
			app.UseSwagger();
			app.UseSwaggerUi3();
		}
	}
}