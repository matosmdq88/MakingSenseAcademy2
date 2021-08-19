using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3
{
    class Program
    {
        static void Main(string[] args)
        {
            //Implicit type conversion
            byte num1 = 15;
            int num2 = num1;

            Console.WriteLine(num2);

            //Explicit type conversion
            float num3 = 19.0f;
            long num4 = (long)num3; //here we do casting from float to long type

            Console.WriteLine(num4);

            //Conversion between NON-compatible types

            var numStr = "1526";
            int num5 = Convert.ToInt32(numStr); //here we convert from string to int type

            Console.WriteLine(num5);

            try  //try catch block used to handle exeption
            {
                var numStr1 = "1526";
                byte num6 = Convert.ToByte(numStr1); //here we convert from string to byte type
            }
            catch (Exception)
            {
                Console.WriteLine("imposible to convert from string to byte because number is above 255");
            }
        }
    }
}
