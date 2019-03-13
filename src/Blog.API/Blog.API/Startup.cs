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
				options.DefaultApiVersion = new ApiVersion(1, 0);
			});
			services.AddVersionedApiExplorer(options => { options.GroupNameFormat = "VV"; });
			
			services.AddMvcCore(opt =>
				{
					opt.Filters.Add(typeof(TransactionFilter));
				})
				.AddCors()
				.AddJsonFormatters()
				.AddFluentValidation(x =>
				{
					x.RegisterValidatorsFromAssemblyContaining<Startup>();
					x.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
				})
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

			services.AddScoped<IConverter<string, string>, MarkdownConverter>();
			
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