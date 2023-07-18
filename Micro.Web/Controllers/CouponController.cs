using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Web.Models;
using Micro.Web.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Micro.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        //Displaying all the coupons

        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto>? list = new();

            ResponseDto? response = await _couponService.GetAllCouponsAsync();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            }
          
            return View(list);
        }

        public async Task<IActionResult> CouponCreate()
        {
            return View();
        }
    }
}

