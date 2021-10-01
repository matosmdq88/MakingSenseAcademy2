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
        private readonly MyDbContext _context;

        public ClientHelper(IConfiguration iconfiguration,MyDbContext context)
        {
            _configuration = iconfiguration;
            _context = context;
        }
        public List<Client> GetAll()
        {
            return _context.Client.ToList();
        }

        public void Add(Client client)
        {
            _context.Client.Add(client);
            _context.SaveChanges();
        }

        public Client GetByDni(int dni)
        {
            return _context.Client.FirstOrDefault(c => c.Dni==dni);
        }

        public bool ValidateDni(ClientRequest model)
        {
            var clients = GetAll();
            if (clients == null)
            {
                return false;
            }
            return clients.Any(client => client.Dni == model.Dni);
        }

        public void Update(Client client)
        {
            _context.Client.Update(client);
            _context.SaveChanges();
        }

        public void Delete(Client client)
        {
            _context.Update(client);
            _context.SaveChanges();
        }
    }
}
