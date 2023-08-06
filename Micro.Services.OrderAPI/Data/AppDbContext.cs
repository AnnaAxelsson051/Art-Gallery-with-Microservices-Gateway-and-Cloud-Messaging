﻿using System;
using Micro.Services.OrderAPI.Models;

using Micro.Services.OrderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Micro.Services.OrderAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

    }
}