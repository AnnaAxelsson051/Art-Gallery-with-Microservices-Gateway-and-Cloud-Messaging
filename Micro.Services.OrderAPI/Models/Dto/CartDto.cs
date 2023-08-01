using System;
using System.Collections.Generic;

namespace Micro.Services.OrderAPI.Models.Dto
{
	public class CartDto
	{
		public CartHeaderDto CartHeader { get; set; }
		public IEnumerable <CartDetailsDto>? CartDetails { get; set; }
	}
}

