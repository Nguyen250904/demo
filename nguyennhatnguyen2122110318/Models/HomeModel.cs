using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nguyennhatnguyen2122110318.Context;

namespace nguyennhatnguyen2122110318.Models
{
    public class HomeModel
    {
        public List<Product> ListProduct {  get; set; }
        public List<Category> ListCategory { get; set; }
    }
}