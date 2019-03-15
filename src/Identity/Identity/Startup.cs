using System;
using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Identity
{
	public class Startup
	{
		private IHostingEnvironment Environment { get; }

		public Startup(IHostingEnvironment environment)
		{
			Environment = environment;
		}
		
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
			
			var builder = services.AddIdentityServer()
				.AddInMemoryApiResources(Config.GetApis())
				.AddInMemoryClients(Config.GetClients())
				.AddTestUsers(Config.GetUsers());

			if (Environment.IsDevelopment())
			{
				builder.AddDeveloperSigningCredential();
			}
			else
			{
				throw new Exception("need to configure key material");
			}
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseStaticFiles();
			app.UseIdentityServer();
			app.UseMvcWithDefaultRoute();
		}
	}
}