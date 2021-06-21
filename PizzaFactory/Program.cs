using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PizzaFactory
{
    class Program
    {
        private static List<Pizza> PizzasToBake = new List<Pizza>();

        static async Task Main(string[] args)
        {

            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("factoryConfig.json").Build();

            var section = config.GetSection(nameof(PizzaFactoryConfig));
            var factoryConfig = section.Get<PizzaFactoryConfig>();
            var logFileName = factoryConfig.LogFileName.Replace("{datetime}", DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss"));

            for (var i = 0; i < factoryConfig.NumberOfPizzas; i++)
            {
                Pizza pizza = PizzaFactory.RandomPizza(factoryConfig);
                pizza.TotalCookTime = PizzaFactory.CalculatedCookingTime(factoryConfig.BaseCookingTime, pizza.BaseCookingTimeMultiplier, pizza.Topping);

                PizzasToBake.Add(pizza);
            }

            if (factoryConfig.Interval != 0)
            {
                await CookAllPizzas(logFileName, factoryConfig.Interval);
            }
            else
            {
                await CookAllPizzas(logFileName);
            }

        }

        private static async Task CookPizza(Pizza pizza)
        {
            await Task.Delay(pizza.TotalCookTime);
            Log.Information("Pizza cooked: " + pizza.Base + " with " + pizza.Topping + "(Cooking Time (seconds): " + (pizza.TotalCookTime / 1000).ToString("#.##") + ")");
        }

        private static async Task CookAllPizzas(string logFileName, int interval)
        {
            
            //initiate logging
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(logFileName)
                .CreateLogger();

            //start pizza making!
            Log.Information("PIZZA FACTORY STARTED");

            foreach (Pizza pizza in PizzasToBake)
            {
                await Task.Delay(interval);
                await CookPizza(pizza);
            }

            Log.Information("PIZZA FACTORY FINISHED");
        }

        private static async Task CookAllPizzas(string logFileName)
        {
            //initiate logging
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(logFileName)
                .CreateLogger();

            //start pizza making!
            Log.Information("PIZZA FACTORY STARTED");

            foreach (Pizza pizza in PizzasToBake)
            {
                await CookPizza(pizza);
            }

            Log.Information("PIZZA FACTORY FINISHED");
        }

    }
}
