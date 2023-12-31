﻿using System;
using System.Text;
using Micro.Services.EmailAPI.Data;
using Micro.Services.EmailAPI.Message;
using Micro.Services.EmailAPI.Models;
using Micro.Services.EmailAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Micro.Services.EmailAPI.Services
{
	public class EmailService : IEmailService
	{
        private DbContextOptions<AppDbContext> _dbOptions;

        public EmailService(DbContextOptions<AppDbContext> dbOptions)
        {
            _dbOptions = dbOptions;
        }

        //Tamplate for email
        public async Task EmailCartAndLog(CartDto cartDto)
        {
            StringBuilder message = new StringBuilder();

            message.AppendLine("<br/>Cart Email Requested ");
            message.AppendLine("<br/>Total " + cartDto.CartHeader.CartTotal);
            message.Append("<br/>");
            message.Append("<ul>");
            foreach (var item in cartDto.CartDetails)
            {
                message.Append("<li>");
                message.Append(item.Product.Name + " x " + item.Count);
                message.Append("</li>");
            }
            message.Append("</ul>");

            await LogAndEmail(message.ToString(), cartDto.CartHeader.Email);

        }

        //Logging and sending order placed email
        public async Task LogOrderPlaced(RewardsMessage rewardsDto)
        {
            string message = "New order places. <br/> Order ID:" + rewardsDto.OrderId; ;
            await LogAndEmail(message, "a.axelsson51@gmail.com");
        }

        //Registering user email and logging the event
        public async Task RegisterUserEmailAndLog(string email)
        {
            string message = "User Registration Successful. <br/> Email:" + email;
            await LogAndEmail(message, "a.axelsson51@gmail.com");
        }

        //Logging the email
        private async Task<bool> LogAndEmail(string message, string email)
        {
            try
            {
                EmailLogger emailLog = new()
                {
                    Email = email,
                    EmailSent = DateTime.Now,
                    Message = message
                };
                await using var _db = new AppDbContext(_dbOptions);
                await _db.EmailLoggers.AddAsync(emailLog);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

