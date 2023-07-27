using System;
using System.Collections.Generic;

namespace Micro.Services.ShoppingCartAPI.Models.Dto
{
	public class CartDto
	{
		public CartHeaderDto CartHeader { get; set; }
		public IEnumerable <CartDetailsDto>? CartDetails { get; set; }
	}
}

