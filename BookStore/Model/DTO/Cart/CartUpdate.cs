﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Model.DTO.Cart
{
    public class CartUpdate
    {
        public Guid Book { get; set; }
        public int Quantity { get; set; }
    }
}
