using System;
namespace Micro.MessageBus
{
	public interface IMessageBus
	{
		Task PublishMessage(object message, string topic_queue_Name);
	}
}

