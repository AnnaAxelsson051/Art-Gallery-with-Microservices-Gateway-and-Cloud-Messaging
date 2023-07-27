using System;
using Micro.Services.ShoppingCartAPI.Models.Dto;

namespace Micro.Services.ShoppingCartAPI.Service.IService
{
	//Loading products from product api
	public interface ICouponService
	{
		Task<IEnumerable<ProductDto>> GetProducts();
	}
}

