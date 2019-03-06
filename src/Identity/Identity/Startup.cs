using System;
using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Identity
{
	public class Startup
	{
		public IHostingEnvironment Environment { get; }

		public Startup(IHostingEnvironment environment)
		{
			Environment = environment;
		}
		
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);

			var builder = services.AddIdentityServer()
				.AddInMemoryIdentityResources(Config.GetIdentityResources())
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

			services.AddAuthentication()
				.AddOpenIdConnect("oidc", "OpenID Connect", options =>
				{
					options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
					options.SignOutScheme = IdentityServerConstants.SignoutScheme;
					options.SaveTokens = true;

					options.Authority = "https://demo.identityserver.io/";
					options.ClientId = "implicit";

					options.TokenValidationParameters = new TokenValidationParameters
					{
						NameClaimType = "name",
						RoleClaimType = "role"
					};
				});
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