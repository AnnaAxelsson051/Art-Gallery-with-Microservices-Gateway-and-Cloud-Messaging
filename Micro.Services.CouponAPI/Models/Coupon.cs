﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Micro.Services.CouponAPI.Models
{
	public class Coupon
	{
      
        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }
}

