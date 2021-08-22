using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_5.Models
{
    public class Male : Person
    {
        public override void Greet()
        {
            Console.WriteLine("Hi, my name is "+name+", i´m "+age+" years old and i´m Man" );
        }
    }
}
