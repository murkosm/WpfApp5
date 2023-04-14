using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;

namespace WpfApp5.DB
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
    }
}