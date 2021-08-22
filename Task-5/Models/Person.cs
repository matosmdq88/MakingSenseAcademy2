using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_5.Models
{
    public abstract class Person
    {
        public string name { get; set; }
        public int age { get; set; }

        public abstract void Greet();
    }
}
