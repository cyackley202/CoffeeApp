using Microsoft.AspNetCore.Mvc;
using CoffeeAppRevisted.Service;
using CoffeeAppRevisted.Models.Coffee;
using System.Collections.Generic;
using System.Linq;
using CoffeeAppRevisted.Models.Coffee.Repository;
using System.Threading.Tasks;

namespace CoffeeAppRevisted.Controllers
{
    public class CoffeeController : Controller
    {
        private readonly ICoffeeRepository _coffeeRepository;

        public CoffeeController(ICoffeeRepository coffeeRepository)
        {
            _coffeeRepository = coffeeRepository;
        }

        public async Task<IActionResult> CoffeesMenu()
        {
            var model = new CoffeesViewModel();
            var coffeesDBO = await _coffeeRepository.SelectAllCoffee();
            model.Coffees = coffeesDBO.Select(coffeeDBO => coffeeDBO).ToList();

            return View(model);
        }

        public IActionResult GenerateCoffee()
        {
            var model = new GenerateCoffeeViewModel();
            return View(model);
        }

        public async Task<IActionResult> InsertCoffee(GenerateCoffeeViewModel coffee)
        {
            var model = new CoffeeDBO();
            model.Name = coffee.Name;
            model.Description = coffee.Description;
            model.Price = coffee.Price;

            await _coffeeRepository.InsertIntoCoffee(model);

            return RedirectToAction("CoffeesMenu");

        }


        //    public IActionResult CoffeesMenu()
        //    {
        //        var model = new CoffeesViewModel();
        //        model.Coffees = _coffeeRepository.GetAllCoffee();

        //        return View(model);
        //    }

        //    public IActionResult AddCoffee()
        //    {
        //        var model = new Coffee("Coffee", "Description", 0, 0);
        //        return View(model);
        //    }

        //    public IActionResult AddedCoffee(string name, string description, int id, double price)
        //    {
        //        Coffee coffee = new Coffee(name, description, id, price);
        //        _coffeeRepository.AddCoffee(coffee);
        //        return RedirectToAction(nameof(CoffeesMenu));
        //    }

        public  IActionResult DeleteCoffee() 
        {
            var model = new DeleteCoffeeViewModel();
            return View(model);
        }

        public IActionResult UpdateCoffee() 
        {
            var model = new UpdateCoffeeViewModel();
            return View(model);
           

        }

        public async Task<IActionResult> UpdatedCoffee(UpdateCoffeeViewModel coffee)
        {
            var model = new CoffeeDBO();
            model.Name = coffee.Name;
            model.Description = coffee.Description;
            model.Price = coffee.Price;
            model.ID = coffee.ID;

            await _coffeeRepository.UpdateExistingCoffee(model);

            return RedirectToAction("CoffeesMenu");
        }

        public async Task<IActionResult> PurgeCoffee(DeleteCoffeeViewModel coffee)
        {
            var model = new CoffeeDBO();
            model.ID = coffee.ID;

            await _coffeeRepository.DeleteExistingCoffee(model);

            return RedirectToAction("CoffeesMenu");
        }



        public async Task<IActionResult> ShowCoffeeDetails(int ID)  
        {
            var model = new CoffeeDBO();
            var viewModel = new ShowCoffeeDetailsViewModel();
            model.ID = ID;
            var showMeThisCoffee = await _coffeeRepository.ShowCoffeeDetails(model);

            viewModel.Description = showMeThisCoffee.Description;
            viewModel.Name = showMeThisCoffee.Name;
            viewModel.ID = showMeThisCoffee.ID;
            viewModel.Price = showMeThisCoffee.Price;

            return View(viewModel);
        }
    }
}

