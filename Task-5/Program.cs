using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_5.Models;

namespace Task_5
{
    class Program
    {
        static void Main(string[] args)
        {
            Person male = new Male();
            male.name = "Matias Fernandez";
            male.age = 33;

            Person female = new Female();
            female.name = "Candela Caballero de Tineo";
            female.age = 28;

            List<Person> myList = new List<Person>();
            myList.Add(male);
            myList.Add(female);

            foreach (Person aux in myList)
            {
                aux.Greet();
            }
        }
    }
}
