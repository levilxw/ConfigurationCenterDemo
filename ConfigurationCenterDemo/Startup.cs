using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using SchoolPal.DisConf.Client.Configuration;
using Swashbuckle.AspNetCore.Swagger;

namespace ConfigurationCenterDemo
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
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json", false, true);
            var configuration = configurationBuilder.Build();

            configurationBuilder.AddDisConf(x =>
            {
                x.App = configuration["app:name"];
                x.AppVersion = configuration["app:version"];
                x.Cluster = configuration["app:cluster"];
                x.Env = configuration["app:env"];
                x.Token = configuration["app:token"];
                x.Server = configuration["app:server"];
            });

            configuration = configurationBuilder.Build();

            var ossConfig = new OssConfig();
            configuration.Bind("OssConfig", ossConfig);
            services.AddSingleton(typeof(OssConfig), ossConfig);

            ChangeToken.OnChange(() => configuration.GetReloadToken(), configurationRoot =>
            {
                configuration.Bind("OssConfig", services.BuildServiceProvider().GetService<OssConfig>());
            }, configuration);

            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "ConfigurationCenterDemo API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
