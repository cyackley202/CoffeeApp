using CoffeeAppRevisted.Config;
using CoffeeAppRevisted.Models.Coffee;
using CoffeeAppRevisted.Models.Coffee.Repository;
using Dapper;
using Identity.Dapper.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeAppRevisted.Service
{
    public class CoffeeRepository : ICoffeeRepository
    {

        private string _connectionString;

        public CoffeeRepository(IOptions<ConnectionProviderOptions> config)
        {
            _connectionString = config.Value.ConnectionString;
        }


        

        public static List<Coffee> GetInitialCoffees()
        {
            var coffees = new List<Coffee>()
            {
                new Coffee("Cafe Con Leche", "A Spanish twist on a French classic!", 1, 5.00),
                new Coffee("Nilla", "It's everyone's favorite for a reason!", 2, 4.00),
                new Coffee("Mocha Choca Latta Yaya", "A Moulin Rouge staple!", 3, 5.00),
                new Coffee("Yummy Boys Coffee", "Wait and see how he stirs it!", 4, 2.00)

            };

            return coffees;
        }

   

        public async Task<IEnumerable<CoffeeDBO>> SelectAllCoffee()
        {
            const string queryString = "Select * from [dbo].Coffee";

            using (var connection =  new SqlConnection(_connectionString))
            {
                IEnumerable<CoffeeDBO> orderDetail = await connection.QueryAsync<CoffeeDBO>(queryString);

                return orderDetail;
            }


        }

        //@"INSERT INTO Coffee (Name, Price, Description)
        //                        VALUES(@{nameof(CoffeeDBO.Name}, @{nameof(CoffeeDBO.Price}, @{nameof(CoffeeDBO.Description};"

        public async Task<bool> InsertIntoCoffee(CoffeeDBO model)
        {
            var queryString = @$"INSERT INTO Coffee (Name, Price, Description) 
                                VALUES(@{nameof(CoffeeDBO.Name)}, @{nameof(CoffeeDBO.Price)}, @{nameof(CoffeeDBO.Description)});";

            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var orderDetail = await connection.ExecuteAsync(queryString, model);
                    return true;

                }
                catch
                {
                    return false;
                }

            }
        }

        public async Task<bool> UpdateExistingCoffee(CoffeeDBO model)
        {
            var queryString = @$"UPDATE Coffee
                                SET Name = @{nameof(CoffeeDBO.Name)}, Price = @{nameof(CoffeeDBO.Price)}, Description = @{nameof(CoffeeDBO.Description)}
                                WHERE ID = @{nameof(CoffeeDBO.ID)}";

            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var orderDetail = await connection.ExecuteAsync(queryString, model);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public async Task<bool> DeleteExistingCoffee(CoffeeDBO model)
        {
            var queryString = @$"DELETE FROM Coffee 
                                WHERE ID = @{nameof(CoffeeDBO.ID)}";


            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var orderDetail = await connection.ExecuteAsync(queryString, model);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }


        public async Task<CoffeeDBO> ShowCoffeeDetails(CoffeeDBO model) //model DOES contain name
        {

            CoffeeDBO newOne = new CoffeeDBO();

            var queryString = @$"Select * FROM Coffee 
                                WHERE ID = @{nameof(CoffeeDBO.ID)}"; 

                                //declare @name varchar(255) = 'Sugar Bomb'
                                //Select * FROM Coffee
                                //WHERE Name = @name


            using (var connection = new SqlConnection(_connectionString))
            {

                var orderDetail = (await connection.QueryAsync<CoffeeDBO>(queryString, model)).FirstOrDefault();

                return orderDetail;
                
            }
        }
    }
}