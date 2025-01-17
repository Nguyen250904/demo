using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nguyennhatnguyen2122110318.Context;

namespace nguyennhatnguyen2122110318.Models
{
    public class CartModel
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}