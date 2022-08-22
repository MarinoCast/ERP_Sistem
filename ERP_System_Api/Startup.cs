namespace ERP_System_Api
{

    //Startup the configuration Extension for call the dependencies
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

       public void ConfigureServices(IServiceCollection services)
        {
            services.ServicesAssembly(Configuration);

        }
        public void Configure(IApplicationBuilder app, IHostApplicationLifetime lifetime)
        {
            
            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            app.UseSwagger(option => { option.RouteTemplate = swaggerOptions.JsonRoute; });

            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description);
            });

        }
    }
}
