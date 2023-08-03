using System;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Text;
using Micro.Services.RewardAPI.Services;
using Newtonsoft.Json;
using Azure.Messaging.ServiceBus;
using Micro.Services.RewardAPI.Message;

namespace Micro.Services.RewardAPI.Messaging

//Consuming messages from an Azure Service Bus topic subscription
//Initializing a ServiceBusProcessor to process messages from
//a specified topic and subscription, allowing the RewardService
//to handle the incoming messages async
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
	{
	private readonly string serviceBusConnectionString;
    private readonly string orderCreatedTopic;
    private readonly string orderCreatedRewardSubscription;
    private readonly IConfiguration _configuration;
    private readonly RewardService _rewardService;

        private ServiceBusProcessor _rewardProcessor;

        public AzureServiceBusConsumer (IConfiguration configuration, RewardService rewardService)
		{
            _rewardService = rewardService;
			_configuration = configuration;
			serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
            orderCreatedTopic = _configuration.GetValue<string>("TopicAndQueueNames:OrderCreatedTopic");
            orderCreatedRewardSubscription = _configuration.GetValue<string>("TopicAndQueueNames:OrderCreated_Rewards_Subscription");

            var client = new ServiceBusClient(serviceBusConnectionString);
			_rewardProcessor = client.CreateProcessor(orderCreatedTopic, orderCreatedRewardSubscription);
        }

        //Starts processing messages 
        public async Task Start()
        {
            _rewardProcessor.ProcessMessageAsync += OnNewOrderRewardsRequestReceived;
            _rewardProcessor.ProcessErrorAsync += ErrorHandler;
            await _rewardProcessor.StartProcessingAsync();
        }

        public async Task Stop()
        {
            await _rewardProcessor.StopProcessingAsync();
            await _rewardProcessor.DisposeAsync();
        }

        //Handeling a new message received event in Azure Service Bus consumer
        //Deserializing message payload, updating rewards based on message content
        //and  completes message processing
        private async Task OnNewOrderRewardsRequestReceived(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);

            RewardsMessage objMessage = JsonConvert.DeserializeObject<RewardsMessage>(body);
            try
            {
                await _rewardService.UpdateRewards(objMessage);
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


