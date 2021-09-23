using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RentallCarsAPI.Models;
using RentallCarsAPI.Models.Request;
using RentallCarsAPI.Tools.Interfaces;

namespace RentallCarsAPI.Tools
{
    public class ClientHelper : IClientHelper
    {
        private readonly IConfiguration _configuration;

        public ClientHelper(IConfiguration iconfiguration)
        {
            _configuration = iconfiguration;
        }
        public List<Client> GetAll()
        {
            var clients = new List<Client>();
            if (!System.IO.File.Exists(_configuration.GetValue<string>("MySettings:_pathclients")))
            {
                return clients;
            }
            try
            {
                var list = System.IO.File.ReadAllText(_configuration.GetValue<string>("MySettings:_pathclients"));
                if (list == "")
                    return clients;
                else
                {
                    clients= JsonConvert.DeserializeObject<List<Client>>(list);
                    return clients.OrderBy(c => c.Dni).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Client GetByDni(int Dni)
        {
            var clients = GetAll();
            if (clients == null)
                return null;
            else
                return clients.FirstOrDefault(client => client.Dni == Dni);
        }

        public bool ValidateDni(ClientRequest model)
        {
            var clients = GetAll();
            if (clients == null)
                return false;
            else
                return clients.Any(client => client.Dni == model.Dni);
        }
    }
}
