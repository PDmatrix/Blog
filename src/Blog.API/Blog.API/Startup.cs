using Blog.API.Application.Interfaces;
using Blog.API.Application.Posts.Queries;
using Blog.API.Persistence;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

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
			services.AddApiVersioning(options =>
			{
				options.AssumeDefaultVersionWhenUnspecified = true;
				options.ApiVersionReader = new UrlSegmentApiVersionReader();
			});
			services.AddVersionedApiExplorer(options => { options.GroupNameFormat = "VV"; });

			services.AddMvcCore()
				.AddCors()
				.AddJsonFormatters()
				.AddJsonOptions(x =>
				{
					x.SerializerSettings.ContractResolver = new DefaultContractResolver
					{
						NamingStrategy = new SnakeCaseNamingStrategy()
					};
				})
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
				
			services.AddScoped<IUnitOfWorkFactory>(provider => 
					new UnitOfWorkFactory(Config.GetConnectionString("DefaultConnection")));

			services.AddMediatR(typeof(GetAllPostsHandler));
			services.AddSwaggerDocument(options =>
			{
				options.PostProcess = document =>
				{
					document.Info.Version = "1.0";
					document.Info.Title = "My Blog";
					document.Info.Description = "My personal blog";
					document.Info.License = new NSwag.SwaggerLicense
					{
						Name = "MIT",
						Url = "https://github.com/PDmatrix/Blog/blob/master/LICENSE"
					};
				};
			});
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
			
			app.UseSwagger();
			app.UseSwaggerUi3();
		}
	}
}