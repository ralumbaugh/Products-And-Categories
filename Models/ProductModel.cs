using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
namespace ProductsAndCategories.Models
{
    public class Product
    {
        [Key]
        public int ProductId {get;set;}
        [Required (ErrorMessage="A name is required")]
        public string Name {get;set;}
        [Required (ErrorMessage="A description is required")]
        public string Description {get; set;}
        [Required (ErrorMessage="A price is required")]
        [DataType (DataType.Currency, ErrorMessage="Please input a price in USD")]
        [Range (0,double.PositiveInfinity, ErrorMessage="Please input a positive Price")]
        public double? Price {get; set;}
        public List<Association> Associations {get; set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}