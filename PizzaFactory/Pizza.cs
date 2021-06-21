using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaFactory
{
    public class Pizza
    {
        public string Base { get; set; }
        public string Topping { get; set; }
        public Decimal BaseCookingTimeMultiplier { get; set; } 
        public int TotalCookTime { get; set; }
    }
}
