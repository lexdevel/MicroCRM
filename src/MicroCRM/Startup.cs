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
using Microsoft.Extensions.Hosting;
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
        #region Private members

        private readonly IWebHostEnvironment _webHostEnvironment;

        #endregion

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
        /// <param name="webHostEnvironment">The web host environment.</param>
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
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
                    dbContextOptionsBilder.UseNpgsql(Configuration["DB_CONNECTION_STRING"]);
                    dbContextOptionsBilder.EnableSensitiveDataLogging(_webHostEnvironment.IsDevelopment() || _webHostEnvironment.IsStaging());
                });

            serviceCollection
                .AddControllers()
                .AddNewtonsoftJson(mvcNewtonsoftJsonOptions =>
                {
                    mvcNewtonsoftJsonOptions.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    mvcNewtonsoftJsonOptions.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

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
            applicationBuilder.UseDefaultFiles();
            applicationBuilder.UseStaticFiles();

            if (webHostEnvironment.IsDevelopment())
            {
                applicationBuilder.UseSpaStaticFiles();
            }

            applicationBuilder.UseRouting();
            applicationBuilder.UseAuthentication();
            applicationBuilder.UseAuthorization();
            applicationBuilder.UseEndpoints(endpointRouteBuilder => endpointRouteBuilder.MapControllers());

            if (webHostEnvironment.IsDevelopment())
            {
                applicationBuilder.UseSpa(spaBuilder =>
                {
                    spaBuilder.Options.SourcePath = "WebApp";
                    spaBuilder.UseAngularCliServer(npmScript: "start");
                });
            }
        }

        #endregion
    }
}
