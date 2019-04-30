using System;

using Autofac;
using Autofac.Extensions.DependencyInjection;

using IRU.Common.WebApplication;
using IRU.LargeFileUploader.WebApp.IoC;
using IRU.Services.DependencyInjection;
using IRU.Services.Parsers.DependencyInjection;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Swashbuckle.AspNetCore.Swagger;

namespace IRU.LargeFileUploader.WebApp
{
    public class Startup
    {
        private WebContainerBuilder _containerBuilder;

        private WebContainerBuilder ContainerBuilder => this.GetWebContainerBuilder();

        public Startup(IHostingEnvironment env)
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            this.Configuration = configBuilder.Build();
        }

        //public IContainer ApplicationContainer { get; private set; }

        public IConfigurationRoot Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Large File Uploader",
                    Description = "REST API solution to upload larger CSV files",
                    TermsOfService = "None",
                    Contact = new Contact()
                    {
                        Name = "Leonte Levinta",
                        Email = "llevintza@gmail.com"
                    }
                });
            });
        }

        // ConfigureServices is where you register dependencies. This gets
        // called by the runtime before the Configure method, below.
        //public IServiceProvider ConfigureServices(IServiceCollection services)
        //{
        //    // Add services to the collection.
        //    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        //    services.AddSwaggerGen(c => {
        //        c.SwaggerDoc("v1", new Info
        //        {
        //            Version = "v1",
        //            Title = "Large File Uploader",
        //            Description = "REST API solution to upload larger CSV files",
        //            TermsOfService = "None",
        //            Contact = new Contact()
        //            {
        //                Name = "Leonte Levinta",
        //                Email = "llevintza@gmail.com"
        //            }
        //        });
        //    });
        //    // Create the container builder.
        //    var builder = new ContainerBuilder();

        //    // Register dependencies, populate the services from
        //    // the collection, and build the container.
        //    //
        //    // Note that Populate is basically a foreach to add things
        //    // into Autofac that are in the collection. If you register
        //    // things in Autofac BEFORE Populate then the stuff in the
        //    // ServiceCollection can override those things; if you register
        //    // AFTER Populate those registrations can override things
        //    // in the ServiceCollection. Mix and match as needed.
        //    builder.Populate(services);
        //    builder.RegisterType<DataService>().As<IDataService>();
        //    this.ApplicationContainer = builder.Build();

        //    // Create the IServiceProvider based on the container.
        //    return new AutofacServiceProvider(this.ApplicationContainer);
        //}

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            this.ContainerBuilder.InitializeAutoFac(containerBuilder);
            this.ContainerBuilder.InitializeAutoMapper(containerBuilder);   
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env,
            ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            //app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(context => context.SwaggerEndpoint("/swagger/v1/swagger.json", "V1"));
        }

        private WebContainerBuilder GetWebContainerBuilder()
        {
            if (this._containerBuilder == null)
            {
                this._containerBuilder = new WebContainerBuilder();
                this.RegisterModules(this._containerBuilder);
            }

            return this._containerBuilder;
        }

        private void RegisterModules(WebContainerBuilder builder)
        {
            builder.RegisterModule<WebAppModule>();
            builder.RegisterModule<ServicesModule>();
            builder.RegisterModule<ParserModule>();
        }
    }
}
