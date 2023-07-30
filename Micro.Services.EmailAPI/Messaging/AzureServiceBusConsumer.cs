using System;
using System.Text;
using Azure.Messaging.ServiceBus;
using Micro.Services.EmailAPI.Models.Dto;
using Newtonsoft.Json;

namespace Micro.Services.EmailAPI.Messaging
{ 
	public class AzureServiceBusConsumer : IAzureServiceBusConsumer
	{
	private readonly string serviceBusConnectionString;
    private readonly string emailCartQueue;
    private readonly IConfiguration _configuration;

		private ServiceBusProcessor _emailCartProcessor;

		public AzureServiceBusConsumer (IConfiguration configuration)
		{
			_configuration = configuration;
			serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
			emailCartQueue = _configuration.GetValue<string>("TopicAndQueueNames:EmailShoppingCartQueue");

			var client = new ServiceBusClient(serviceBusConnectionString);
			_emailCartProcessor = client.CreateProcessor(emailCartQueue);
		}

        public async Task Start()
        {
            _emailCartProcessor.ProcessMessageAsync += OnEmailCartRequestReceived;
            _emailCartProcessor.ProcessErrorAsync += ErrorHandler;
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

        private async Task OnEmailCartRequestReceived(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);

            CartDto objMessage = JsonConvert.DeserializeObject<CartDto>(body);
            try
            {
                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task Stop()
        {
            throw new NotImplementedException();
        }
    }	
	}


