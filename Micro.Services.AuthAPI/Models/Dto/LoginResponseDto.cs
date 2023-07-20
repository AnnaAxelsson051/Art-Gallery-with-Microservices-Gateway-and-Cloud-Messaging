using System;
namespace Micro.Services.AuthAPI.Models.Dto
{
	public class LoginResponseDto
	{
		public UserDto User { get; set; }
        public string Token { get; set; }
    }
}

