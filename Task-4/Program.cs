using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_4
{
    class Program
    {
        static void Main(string[] args)
        {
            //declarate variables and use arithmetic operators
            var a = 45;
            var b = 0;
            var c = b+3;
            var d = (float)a / (float)c;

            Console.WriteLine(a);
            Console.WriteLine(b);
            Console.WriteLine(c);
            Console.WriteLine(d);

            //postfix and prefix increment
            a = 10;
            b = a++;
            Console.WriteLine("postfix");
            Console.WriteLine(a);
            Console.WriteLine(b);

            a = 10;
            b = ++a;
            Console.WriteLine("prefix");
            Console.WriteLine(a);
            Console.WriteLine(b);

            //comparison operators
            a = 10;
            b = 20;
            c = 5;
            Console.WriteLine("Comparison operators");
            Console.WriteLine(a!=b);
            Console.WriteLine(b > a && c==a/2);

            byte number = 255;

            number += 2;

            Console.WriteLine(number);
        }
    }
}
