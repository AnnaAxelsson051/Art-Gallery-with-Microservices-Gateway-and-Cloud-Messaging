﻿using System;
using Micro.Web.Models;

namespace Micro.Web.Service
{
    public interface IOrderService
    {

        Task<ResponseDto?> CreateOrder(CartDto cartDto);
        

    }
}
