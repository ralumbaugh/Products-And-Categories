using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductsAndCategories.Models
{
    public class Wrapper
    {
        public Product NewProduct {get;set;}
        public Category NewCategory {get; set;}
        public List<Product> AllProducts {get; set;}
        public List<Category> AllCategories {get; set;}
    }
}