using System;
using Micro.Services.EmailAPI.Models.Dto;

namespace Micro.Services.EmailAPI.Services
{
	public interface IEmailService
	{
		Task EmailCartAndLog(CartDto cartDto);
        Task RegisterUserEmailAndLog(string email);
    }
}

