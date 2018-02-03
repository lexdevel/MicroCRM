using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using MicroCRM.Data;
using MicroCRM.Services.Encryption;
using MicroCRM.Sessions;
using MicroCRM.Services.Random;
using MicroCRM.Extensions;

namespace MicroCRM
{
    /// <summary>
    /// The startup class.
    /// </summary>
    internal class Startup
    {
        #region Public properties

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// Gets the environment.
        /// </summary>
        public Environment Environment { get; }

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="hostingEnvironment">The hosting environment.</param>
        public Startup(IHostingEnvironment hostingEnvironment)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddApplicationInsightsSettings(developerMode: hostingEnvironment.IsDevelopment())
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddEnvironmentVariables();

            Configuration = configurationBuilder.Build();
            Environment = Environment.TryParse(hostingEnvironment.EnvironmentName) ?? Environment.Development;
        }

        #region Configuration methods

        /// <summary>
        /// Configures the service collection (the DI container).
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            if (Environment != Environment.Development)
            {
                serviceCollection.Configure<MvcOptions>(mvcOptions =>
                {
                    mvcOptions.CacheProfiles.Add("Default", new CacheProfile { Duration = Defaults.ResponseCacheDurationSeconds });
                    mvcOptions.Filters.Add(new ResponseCacheAttribute { CacheProfileName = "Default" });
                    mvcOptions.Filters.Add(new RequireHttpsAttribute { Permanent = true });
                });
            }

            serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwtBearerOptions =>
                {
                    jwtBearerOptions.RequireHttpsMetadata = !Environment.IsDevelopment();
                    jwtBearerOptions.SaveToken = true;
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Defaults.JwtSecret)),
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = true,
                        ValidAudience = Defaults.JwtIssuer,
                        ValidIssuer = Defaults.JwtIssuer,
                        ValidateIssuer = true,
                        ValidateLifetime = true
                    };
                });
            serviceCollection.AddDbContext<DataContext>(dbContextOptionsBilder => dbContextOptionsBilder.UseInMemoryDatabase("MicroCRM"));
            serviceCollection.AddMvc().AddJsonOptions(mvcJsonOptions =>
            {
                mvcJsonOptions.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                mvcJsonOptions.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            serviceCollection.AddTransient<IEncryptionService, EncryptionService>(sp => new EncryptionService());
            serviceCollection.AddTransient<IRangomService, RandomService>(sp => new RandomService());
            serviceCollection.AddTransient<ISessionManager, SessionManager>(sp => new SessionManager(sp.GetService<DataContext>(), sp.GetService<IHttpContextAccessor>()));
        }

        /// <summary>
        /// Configures the web application.
        /// </summary>
        /// <param name="applicationBuilder">The application builder.</param>
        /// <param name="hostingEnvironment">The hosting environment.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment, ILoggerFactory loggerFactory)
        {
            if (Environment.IsDevelopment())
            {
                loggerFactory.AddConsole();
                loggerFactory.AddDebug();

                // applicationBuilder.UseDatabaseErrorPage();
                // applicationBuilder.UseDeveloperExceptionPage();
                applicationBuilder.UseBrowserLink();
                applicationBuilder.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions { HotModuleReplacement = true });

                using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
                {
                    var dataCotext = serviceScope.ServiceProvider.GetService<DataContext>();
                    var encryptionService = serviceScope.ServiceProvider.GetService<IEncryptionService>();
                    DemoDataSnapshot.CreateDemoData(dataCotext, encryptionService);
                }
            }
            else
            {
                applicationBuilder.UseRewriter(new RewriteOptions().AddRedirectToHttpsPermanent());
                applicationBuilder.UseResponseCaching();
                applicationBuilder.UseResponseCompression();
            }
            
            applicationBuilder.UseAuthentication();
            applicationBuilder.UseDefaultFiles();
            applicationBuilder.UseStaticFiles();
            applicationBuilder.UseMvc();
        }

        #endregion
    }
}
