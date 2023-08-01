using System;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Text;
using Azure.Messaging.ServiceBus;
using Micro.Services.EmailAPI.Models.Dto;
using Micro.Services.EmailAPI.Services;
using Newtonsoft.Json;

namespace Micro.Services.EmailAPI.Messaging

//sets up a message processor to handle messages from an Azure
//Service Bus queue for email shopping cart requests.
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
	{
	private readonly string serviceBusConnectionString;
    private readonly string emailCartQueue;
    private readonly IConfiguration _configuration;
    private readonly EmailService _emailService;

        private ServiceBusProcessor _emailCartProcessor;

		public AzureServiceBusConsumer (IConfiguration configuration, EmailService emailService)
		{
            _emailService = emailService;
			_configuration = configuration;
			serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
			emailCartQueue = _configuration.GetValue<string>("TopicAndQueueNames:EmailShoppingCartQueue");

			var client = new ServiceBusClient(serviceBusConnectionString);
			_emailCartProcessor = client.CreateProcessor(emailCartQueue);
        }

       // When start is called, the message processor is configured to
       // process incoming messages and handle potential errors
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

        //stops the message processing, disposes the message processor and starts processing again
        public async Task Stop()
        {
            await _emailCartProcessor.StopProcessingAsync();
            await _emailCartProcessor.DisposeAsync();
            await _emailCartProcessor.StartProcessingAsync();
        }

        //Processing an incoming message from an Azure Service Bus queue
        private async Task OnEmailCartRequestReceived(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);

            CartDto objMessage = JsonConvert.DeserializeObject<CartDto>(body);
            try
            {
                await _emailService.EmailCartAndLog(objMessage);
                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }	
	}


