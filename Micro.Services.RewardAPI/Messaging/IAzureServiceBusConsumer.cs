using System;
namespace Micro.Services.RewardAPI.Messaging
{
	public interface IAzureServiceBusConsumer
	{
		Task Start();
		Task Stop();

	}
}

