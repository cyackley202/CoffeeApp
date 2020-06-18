using CoffeeAppRevisted.Models.Coffee.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeAppRevisted.Models.Coffee
{
    public class CoffeesViewModel 
    {
        public IEnumerable<CoffeeDBO> Coffees { get; set; }
    }
}