using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeAppRevisted.Models.Coffee
{
    public class Coffee
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ID { get; set; }
        public double Price{ get; set; }



        public Coffee(string name, string description, int id, double price)
        {
            Name = name;
            Description = description;
            ID = id;
            Price = price;
        }
    }
}
