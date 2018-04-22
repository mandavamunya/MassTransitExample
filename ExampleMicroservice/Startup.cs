using System;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExampleMicroservice
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
            var bus = InitBus();

	        services.AddSingleton<IBus>(s => bus);
	        services.AddSingleton<IBusControl>(s => bus);
	        services.AddSingleton<IPublishEndpoint>(s => bus);

			services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

	        var bus = serviceProvider.GetRequiredService<IBusControl>();
            bus.Start();
            lifetime.ApplicationStopping.Register(() => bus.Stop());

	        //var busHandle = TaskUtil.Await(() => bus.StartAsync());
	        //lifetime.ApplicationStopping.Register(() => busHandle.Stop());
		}

	    private IBusControl InitBus()
	    {
		    return Bus.Factory.CreateUsingRabbitMq(cfg =>
		    {
			    var host = cfg.Host("localhost", "/", c =>
			    {
				    c.Username("guest");
				    c.Password("guest");
			    });

			    cfg.ReceiveEndpoint(host, "UserData", e =>
			    {
				    e.Consumer<ListUserConsumer>();
			    });
			});
	    }
	}
}
