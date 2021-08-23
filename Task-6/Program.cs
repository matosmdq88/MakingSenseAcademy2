using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_6
{
    class Program
    {
        static void Main(string[] args)
        {
            var stringArray = new string[5];
            stringArray[0] = "blue";
            stringArray[1] = "white";
            stringArray[2] = "black";
            stringArray[3] = "red";
            stringArray[4] = "grey";

            for (int a = 0; a < 5; a++)
            {
                Console.WriteLine("the colour is: "+stringArray[a]);
            }
        }
    }
}
