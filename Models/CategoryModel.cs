using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
namespace ProductsAndCategories.Models
{
    public class Category
    {
        [Key]
        public int CategoryId {get;set;}
        [Required (ErrorMessage="A name is required")]
        public string Name {get;set;}
        public List<Association> Associations {get; set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}