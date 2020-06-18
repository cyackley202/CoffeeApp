using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CoffeeAppRevisted.Models.Coffee.Repository;
using CoffeeAppRevisted.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeAppRevisted.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class CoolController : ControllerBase
    {
        private readonly ICoffeeRepository _coffeeRepository;

        public CoolController(ICoffeeRepository coffeeRepository)
        {
            _coffeeRepository = coffeeRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<CoffeeDBO>> SelectAllCoffee()
        {
            var coffees = await _coffeeRepository.SelectAllCoffee();

            return coffees;
        }

        [HttpPost]
        public async Task<IActionResult> InsertIntoCoffee([FromBody] CoffeeDBO model)
        {
            var coffees = _coffeeRepository.InsertIntoCoffee(model).Result;
            // I can't use await here. Is Task and async really needed in an API?
            if (coffees)
            {
                return Ok("Coffee added to database.");
            }
            else
            {
                return BadRequest("Coffee not added to database. Check model properties.");
            }

        }
        [HttpPut]
        public async Task<IActionResult> UpdateExistingCoffee([FromBody] CoffeeDBO model)
        {
            var coffees = _coffeeRepository.UpdateExistingCoffee(model).Result;

            if (coffees)
            {
                return Ok("Coffee updated in database.");
            }
            else
            {
                return BadRequest("Coffee not modified. Check model properties.");
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteExistingCoffee([FromBody] CoffeeDBO model)
        {
            var coffees = _coffeeRepository.DeleteExistingCoffee(model).Result;

            if (coffees)
            {
                return Ok("Coffee removed from database.");
            }
            else
            {
                return BadRequest("Coffee not modified. Check model ID property.");
            }
        }
    }
}