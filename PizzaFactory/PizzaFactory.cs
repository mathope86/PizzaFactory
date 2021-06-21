using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaFactory
{
    public class PizzaFactoryConfig
    {
        public string LogFileName { get; set; }
        public int BaseCookingTime { get; set; }
        public int Interval { get; set; }
        public int NumberOfPizzas { get; set; }
        public List<Base> Bases { get; set; }
        public List<String> Toppings { get; set; }

        public class Base
        {
            public string Name { get; set; }
            public Decimal CookingTimeMultiplier { get; set; }
        }
    }

    public class PizzaFactory
    {


        public static Pizza RandomPizza(PizzaFactoryConfig factoryConfig)
        {
            Random rnd = new Random();
            int rndBase = rnd.Next(factoryConfig.Bases.Count);
            int rndTopping = rnd.Next(factoryConfig.Toppings.Count);

            return new Pizza
            {
                Base = factoryConfig.Bases[rndBase].Name,
                Topping = factoryConfig.Toppings[rndTopping],
                BaseCookingTimeMultiplier = factoryConfig.Bases[rndBase].CookingTimeMultiplier
            };
        }

        public static int CalculatedCookingTime(int BaseCookingTime, Decimal CookingTimeMultiplier, string ToppingName)
        {
            int baseCookingTime = Convert.ToInt32(BaseCookingTime * CookingTimeMultiplier);
            int toppingCookingTime = (100 * ToppingName.Length);

            return baseCookingTime + toppingCookingTime;
        }

    }
}
