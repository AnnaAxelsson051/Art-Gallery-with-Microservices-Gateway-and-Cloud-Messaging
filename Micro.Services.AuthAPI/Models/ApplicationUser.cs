using System;
using Microsoft.AspNetCore.Identity;

namespace Micro.Services.AuthAPI.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string Name { get; set; }
	}
}

