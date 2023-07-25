using System;
using Micro.Services.AuthAPI.Models;

namespace Micro.Services.AuthAPI.Service.IService
{
	public interface IJwtTokenGenerator
	{
		string GenerateToken(ApplicationUser applicationUser, IEnumerable <string> roles);
	}
}

