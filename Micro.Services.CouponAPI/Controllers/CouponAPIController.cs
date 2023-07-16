using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Services.CouponAPI.Data;
using Micro.Services.CouponAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Micro.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponAPIController : Controller
    {
        private readonly AppDbContext _db;

        public CouponAPIController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public object Get()
        {
            try
            {
                IEnumerable<Coupon> objList = _db.Coupons.ToList();
                return objList;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        [HttpGet]
        [Route("{id:int}")]
        public object Get(int id)
        {
            try
            {
                Coupon objList = _db.Coupons.First(u => u.CouponId == id);
                return objList;
            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }
}

