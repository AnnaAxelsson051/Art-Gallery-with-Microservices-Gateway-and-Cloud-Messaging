using System;
namespace Micro.Services.EmailAPI.Messaging
{
	public interface IAzureServiceBusConsumer
	{
		Task Start();
		Task Stop();

	}
}

