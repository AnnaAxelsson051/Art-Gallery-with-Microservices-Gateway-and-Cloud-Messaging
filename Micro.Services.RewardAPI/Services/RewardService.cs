using System;
using System.Text;
using Micro.Services.RewardAPI.Data;
using Micro.Services.RewardAPI.Message;
using Micro.Services.RewardAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Micro.Services.RewardAPI.Services
{
	public class RewardService : IRewardService
	{
        private DbContextOptions<AppDbContext> _dbOptions;

        public RewardService(DbContextOptions<AppDbContext> dbOptions)
        {
            _dbOptions = dbOptions;
        }

        //Updating the rewards info 
        public async Task UpdateRewards(RewardsMessage rewardsMessage)
        {
            try
            {
                Rewards rewards = new()
                {
                    OrderId = rewardsMessage.OrderId,
                    RewardsActivity = rewardsMessage.RewardsActivity,
                    UserId = rewardsMessage.UserId,
                    RewardsDate = DateTime.Now
                };
                await using var _db = new AppDbContext(_dbOptions);
                await _db.Rewards.AddAsync(rewards);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
            }
        }
    }
}

