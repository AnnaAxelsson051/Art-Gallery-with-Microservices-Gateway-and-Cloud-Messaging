using System;
using System.Collections.Generic;

namespace Micro.Web.Models
{
	public class CartDto
	{
		public CartHeaderDto CartHeader { get; set; }
		public IEnumerable <CartDetailsDto>? CartDetails { get; set; }
	}
}

