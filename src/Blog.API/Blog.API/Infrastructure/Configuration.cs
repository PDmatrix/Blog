using System;
using System.Buffers;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NSwag;
using NSwag.SwaggerGeneration.Processors.Security;

namespace Blog.API.Infrastructure
{
	public static class Configuration
	{
		public static void AddCustomMvc(this IServiceCollection services, IHostingEnvironment environment)
		{
			if (services == null)
				throw new ArgumentNullException(nameof (services));

			var builder = services.AddMvcCore(opt =>
			{
				if (environment.IsEnvironment("Testing"))
					opt.Filters.Add(typeof(AllowAnonymousFilter));
				opt.Filters.Add(typeof(TransactionFilter));
				opt.OutputFormatters.Add(new JsonOutputFormatter(new JsonSerializerSettings(), ArrayPool<char>.Shared));
			});
			builder.AddAuthorization();
			builder.AddCors();
			builder.AddFluentValidation(x =>
			{
				x.RegisterValidatorsFromAssemblyContaining<Startup>();
				x.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
			});

			builder.AddJsonOptions(x =>
			{
				x.SerializerSettings.ContractResolver = new DefaultContractResolver
				{
					NamingStrategy = new SnakeCaseNamingStrategy()
				};
			});
			builder.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
		}

		public static void AddCustomSwagger(this IServiceCollection services)
		{
			if (services == null)
				throw new ArgumentNullException(nameof (services));
			
			services.AddSwaggerDocument(options =>
			{
				options.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT"));
				options.DocumentProcessors.Add(new SecurityDefinitionAppender("JWT", new SwaggerSecurityScheme
				{
					Type = SwaggerSecuritySchemeType.ApiKey,
					Name = "Authorization",
					In = SwaggerSecurityApiKeyLocation.Header,
					Description = "Type into the textbox: Bearer {your JWT token}."
				}));
				options.PostProcess = document =>
				{
					document.Info.Version = "1.0";	
					document.Info.Title = "My Blog";
					document.Info.Description = "My personal blog";
					document.Info.License = new SwaggerLicense
					{
						Name = "MIT",
						Url = "https://github.com/PDmatrix/Blog/blob/master/LICENSE"
					};
				};
			});
		}

		public static void AddCustomApiVersioning(this IServiceCollection services)
		{
			if (services == null)
				throw new ArgumentNullException(nameof (services));
			
			services.AddApiVersioning(options =>
			{
				options.AssumeDefaultVersionWhenUnspecified = true;
				options.ApiVersionReader = new UrlSegmentApiVersionReader();
				options.DefaultApiVersion = new ApiVersion(1, 0);
			});
			services.AddVersionedApiExplorer(options => { options.GroupNameFormat = "VV"; });
		}

		public static void AddCustomHttpClientFactory(this IServiceCollection services, string baseAddress)
		{
			services.AddHttpClient("identity", c =>
			{
				c.BaseAddress = new Uri(baseAddress);
			});
		}

		public static void AddCustomAuthentication(this IServiceCollection services, string authority, string audience)
		{
			services.AddAuthentication("Bearer")
				.AddJwtBearer("Bearer", options =>
				{
					options.Authority = authority;
					options.RequireHttpsMetadata = false;

					options.Audience = audience;
				});
		}
	}
}