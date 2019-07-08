using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag.AspNetCore;
using System.Reflection;
using NJsonSchema;
using City.Chain.Insight.Models;
using City.Chain.Insight.Data;
using City.Chain.Insight.Settings;
using City.Chain.Insight.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using City.Chain.Insight.Services;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace City.Chain.Insight
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<NakoSettings>(Configuration.GetSection("Nako"));
            services.AddSingleton<BlockIndexService>();

            services.AddTransient<DatabaseContext>();
            services.AddSingleton<IExchange, Exchanges>();

            services.AddMemoryCache();
            services.AddResponseCaching();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
            });

            // Register the Swagger services
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "City Chain Insight API";
                    document.Info.Description = "API for the City Chain Insight";
                    document.Info.TermsOfService = "https://city-chain.org/terms.html";
                    document.Info.Contact = new NSwag.SwaggerContact
                    {
                        Name = "City Chain Foundation",
                        Email = string.Empty,
                        Url = "https://citychain.foundation"
                    };
                    //document.Info.License = new NSwag.SwaggerLicense
                    //{
                    //    Name = "TBD",
                    //    Url = "https://citychain.foundation"
                    //};
                };
            });

            //services.AddSwaggerGen(
            //  options =>
            //  {
            //      IApiVersionDescriptionProvider provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

            //      foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
            //      {
            //          options.SwaggerDoc(
            //              description.GroupName,
            //                new Info()
            //                {
            //                    Title = $"Stratis.Bitcoin.Api",
            //                    Version = description.ApiVersion.ToString()
            //                });
            //      }

            //      options.OperationFilter<SwaggerDefaultValues>();

            //                  //Set the comments path for the swagger json and ui.
            //                  string basePath = PlatformServices.Default.Application.ApplicationBasePath;
            //      string apiXmlPath = Path.Combine(basePath, "Stratis.Bitcoin.Api.xml");
            //      string walletXmlPath = Path.Combine(basePath, "Stratis.Bitcoin.LightWallet.xml");

            //      if (File.Exists(apiXmlPath))
            //      {
            //          options.IncludeXmlComments(apiXmlPath);
            //      }

            //      if (File.Exists(walletXmlPath))
            //      {
            //          options.IncludeXmlComments(walletXmlPath);
            //      }

            //      options.DescribeAllEnumsAsStrings();
            //  });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Does not work, scrambles the output sometimes. Likely need refactoring for .NET Core 2.2.
            //app.UseApiResponseWrapperMiddleware();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseResponseCaching();

            // Register the Swagger generator and the Swagger UI middlewares
            app.UseSwagger();
            app.UseSwaggerUi3();

            //app.Use(async (context, next) =>
            //{
            //    // For GetTypedHeaders, add: using Microsoft.AspNetCore.Http;
            //    context.Response.GetTypedHeaders().CacheControl =
            //        new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
            //        {
            //            Public = true,
            //            MaxAge = TimeSpan.FromSeconds(10)
            //        };

            //    context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] = new string[] { "Accept-Encoding" };

            //    await next();
            //});

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
