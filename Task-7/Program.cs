using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_7.Models;

namespace Task_7
{
    class Program
    {
        static void Main(string[] args)
        {
            var car = new Car();
            Console.WriteLine("Enter car model");
            car.model = Console.ReadLine();
            Console.WriteLine("Enter mark");
            car.mark = Console.ReadLine();
            Console.WriteLine("Enter Year");
            car.year = int.Parse(Console.ReadLine());
            Console.WriteLine("Increse color: blue=1, red = 2, green = 3, white = 4");
            car.color = (Color)int.Parse(Console.ReadLine());
            Console.WriteLine(car.ToString());
        }
    }
}
