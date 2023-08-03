using System;
using Micro.Services.RewardAPI.Message;

namespace Micro.Services.RewardAPI.Services
{
	public interface IRewardService
	{
		Task UpdateRewards(RewardsMessage rewardsMessage);
     
    }
}

