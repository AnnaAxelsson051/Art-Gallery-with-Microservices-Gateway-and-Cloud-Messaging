using System;
using System.Reflection.Metadata;
using Micro.Services.EmailAPI.Messaging;

namespace Micro.Services.EmailAPI.Extension
{
	public static class ApplicationBuilderExtensions
	{
        //Sets up and starts an Azure Service Bus consumer when application starts and stops the
        //consumer when the application is stopping
        private static IAzureServiceBusConsumer ServiceBusConsumer { get; set; }

		public static IApplicationBuilder UseAzureServiceBusConsumer(this IApplicationBuilder app)
		{
			ServiceBusConsumer = app.ApplicationServices.GetService<IAzureServiceBusConsumer>();
			var hostApplicationLife = app.ApplicationServices.GetService<IHostApplicationLifetime>();

			hostApplicationLife.ApplicationStarted.Register(OnStart);
            hostApplicationLife.ApplicationStopping.Register(OnStop);

            return app;
        }

        private static void OnStop()
        {
            ServiceBusConsumer.Stop();
        }

        private static void OnStart()
        {
            ServiceBusConsumer.Start();
        }
    }
}

