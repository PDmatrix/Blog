using System.Reflection;
using Blog.API.Application.Interfaces;
using Blog.API.Application.Posts.Queries;
using Blog.API.Persistence;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Blog.API
{
	public class Startup
	{
		private IConfiguration Config { get; }
		
		public Startup(IConfiguration config)
		{
			Config = config;
		}
	
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvcCore()
				.AddCors()
				.AddJsonFormatters()
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			services.AddScoped<IUnitOfWorkFactory>(provider => 
					new UnitOfWorkFactory(Config.GetConnectionString("DefaultConnection")));

			services.AddMediatR(typeof(GetAllPostsHandler));
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			app.UseMvc(routes =>
			{
				routes.MapRoute(
					"DefaultApi",
					"api/{controller}/{action}");
			});
		}
	}
}