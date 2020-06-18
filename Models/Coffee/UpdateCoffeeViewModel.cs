using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeAppRevisted.Models.Coffee
{
    public class UpdateCoffeeViewModel //Unsure how to make models interact with objects. Do models pull parameters out of object? 
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
    }
}