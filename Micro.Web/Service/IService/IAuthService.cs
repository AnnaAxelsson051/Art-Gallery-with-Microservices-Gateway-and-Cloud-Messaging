using System;
using Micro.Web.Models;

namespace Micro.Web.Service.IService
{
	public interface IAuthService
	{
		Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);
        Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto);
        Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto);
    }
}

