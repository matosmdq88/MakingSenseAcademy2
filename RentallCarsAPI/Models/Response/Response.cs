using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentallCarsAPI.Models.Response
{
    public class Response
    {
        public bool Succes { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public Response()
        {
            this.Succes = false;
        }
    }
}
