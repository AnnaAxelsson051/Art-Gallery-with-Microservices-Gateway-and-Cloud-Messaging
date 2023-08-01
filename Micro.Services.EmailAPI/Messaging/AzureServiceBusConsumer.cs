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
    private readonly string registerUserQueue;
    private readonly IConfiguration _configuration;
    private readonly EmailService _emailService;

        private ServiceBusProcessor _emailCartProcessor;
        private ServiceBusProcessor _registerUserProcessor;

        public AzureServiceBusConsumer (IConfiguration configuration, EmailService emailService)
		{
            _emailService = emailService;
			_configuration = configuration;
			serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
			emailCartQueue = _configuration.GetValue<string>("TopicAndQueueNames:EmailShoppingCartQueue");
            registerUserQueue = _configuration.GetValue<string>("TopicAndQueueNames:RegisterUserQueue");

            var client = new ServiceBusClient(serviceBusConnectionString);
			_emailCartProcessor = client.CreateProcessor(emailCartQueue);
            _registerUserProcessor = client.CreateProcessor(registerUserQueue);

        }

        //Starts the processing of messages from two
        //Service Bus processors
        public async Task Start()
        {
            _emailCartProcessor.ProcessMessageAsync += OnEmailCartRequestReceived;
            _emailCartProcessor.ProcessErrorAsync += ErrorHandler;
            await _emailCartProcessor.StartProcessingAsync();

            _registerUserProcessor.ProcessMessageAsync += OnUserRegisterRequestReceived;
            _registerUserProcessor.ProcessErrorAsync += ErrorHandler;
            await _registerUserProcessor.StartProcessingAsync();
        }

        //Stops the message processing and disposes message processor 
        public async Task Stop()
        {
            await _emailCartProcessor.StopProcessingAsync();
            await _emailCartProcessor.DisposeAsync();

            await _registerUserProcessor.StopProcessingAsync();
            await _registerUserProcessor.DisposeAsync();
        }

        //Processing an incoming message from an Service Bus queue
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

        //Processesing incoming messages from a Service Bus queue
        //for user registration requests
        private async Task OnUserRegisterRequestReceived(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);

           string email = JsonConvert.DeserializeObject<string>(body);
            try
            {
                await _emailService.RegisterUserEmailAndLog(email);
                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
    }	
	}


