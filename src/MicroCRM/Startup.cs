using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MicroCRM.Auth;
using MicroCRM.Data;
using MicroCRM.Services.Encryption;
using MicroCRM.Services.Random;

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
        public IConfiguration Configuration { get; }

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #region Configuration methods

        /// <summary>
        /// Configures the service collection (the DI container).
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwtBearerOptions =>
                {
                    jwtBearerOptions.RequireHttpsMetadata = false;
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

            serviceCollection
                .AddDbContext<DataContext>(dbContextOptionsBilder =>
                {
                    dbContextOptionsBilder.UseInMemoryDatabase("MicroCRM");
                    dbContextOptionsBilder.EnableSensitiveDataLogging(true);
                });

            serviceCollection
                .AddControllers()
                .AddNewtonsoftJson();

            serviceCollection
                .AddSpaStaticFiles(spaStaticFilesOptions => spaStaticFilesOptions.RootPath = "WebApp/dist");

            serviceCollection.AddTransient<AuthContext>();
            serviceCollection.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddTransient<IEncryptionService, EncryptionService>();
            serviceCollection.AddTransient<IRangomService, RandomService>();
        }

        /// <summary>
        /// Configures the web application.
        /// </summary>
        /// <param name="applicationBuilder">The application builder.</param>
        /// <param name="webHostEnvironment">The webhost environment.</param>
        public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment webHostEnvironment)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var dataCotext = serviceScope.ServiceProvider.GetService<DataContext>();
                var encryptionService = serviceScope.ServiceProvider.GetService<IEncryptionService>();
                DemoDataSnapshot.CreateDemoData(dataCotext, encryptionService);
            }

            applicationBuilder.UseDefaultFiles();
            applicationBuilder.UseStaticFiles();
            applicationBuilder.UseSpaStaticFiles();
            applicationBuilder.UseRouting();
            applicationBuilder.UseAuthentication();
            applicationBuilder.UseAuthorization();
            applicationBuilder.UseEndpoints(endpointRouteBuilder => endpointRouteBuilder.MapControllers());
            applicationBuilder.UseSpa(spaBuilder =>
            {
                spaBuilder.Options.SourcePath = "WebApp";
                spaBuilder.UseAngularCliServer(npmScript: "start");
            });
        }

        #endregion
    }
}
