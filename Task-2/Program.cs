using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var number = 9;
            var firstName = "matias";
            var character = 'F';
            var floatNumber = 27.9f;
            var itsOk = true;
            const float Pi = 3.14f;

            Console.WriteLine(number);
            Console.WriteLine(firstName);
            Console.WriteLine(character);
            Console.WriteLine(floatNumber);
            Console.WriteLine(itsOk);
            Console.WriteLine("Pi value its= "+Pi);
            Console.WriteLine("{0} {1}", int.MinValue,int.MaxValue);
        }
    }
}
