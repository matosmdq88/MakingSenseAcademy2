using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentallCarsAPI.Models.Request
{
    public class ClientRequest
    {
        public Guid Id { get; set; }
        public int Dni { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public short PostalCode { get; set; }
        public bool Available { get; set; }
    }
}
