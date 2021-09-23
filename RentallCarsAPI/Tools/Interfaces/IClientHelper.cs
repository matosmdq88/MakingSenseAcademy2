using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentallCarsAPI.Models;
using RentallCarsAPI.Models.Request;

namespace RentallCarsAPI.Tools.Interfaces
{
    public interface IClientHelper
    {
        public bool ValidateDni(ClientRequest model);
        public List<Client> GetAll();
        public Client GetByDni(int Dni);
    }
}
