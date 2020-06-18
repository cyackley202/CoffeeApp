using CoffeeAppRevisted.Models.Coffee;
using CoffeeAppRevisted.Models.Coffee.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeAppRevisted.Service
{
    public interface ICoffeeRepository
    {
        //void AddCoffee(Coffee coffeeObject);  //Adding Coffee object to list should be objective now. Take in a Coffee object.
        //void DeleteCoffee(int coffeeID); //Removing Coffee object from list should be objective now. Take in an int.
        
        //void UpdateCoffee(string name, string description, int id, double price); //Change output type. Nothing further required.
        //List<Coffee> GetAllCoffee();  //Replace arguments with constructor parameters. ID will pick which object to change. ID will not change.

        Task<IEnumerable<CoffeeDBO>> SelectAllCoffee();

        Task<bool> InsertIntoCoffee(CoffeeDBO model);

        Task<bool> UpdateExistingCoffee(CoffeeDBO model);

        Task<bool> DeleteExistingCoffee(CoffeeDBO model);

        Task<CoffeeDBO> ShowCoffeeDetails(CoffeeDBO model);
    }
}