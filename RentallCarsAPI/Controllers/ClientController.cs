using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RentallCarsAPI.Models;
using RentallCarsAPI.Models.Request;
using RentallCarsAPI.Models.Response;
using RentallCarsAPI.Tools.Interfaces;

namespace RentallCarsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ClientController : Controller
    {
        private readonly IClientHelper _clientHelper;
        private readonly IConfiguration _configuration;

        public ClientController(IClientHelper iClientHelper, IConfiguration iConfiguration)
        {
            _clientHelper = iClientHelper;
            _configuration = iConfiguration;
        }

        [HttpPost]
        public IActionResult Create(ClientRequest model)
        {
            var response = new Response();
            var clients = _clientHelper.GetAll();
            if (clients == null)
            {
                response.Message = "File reading failed";
                return BadRequest(response);
            }

            if (_clientHelper.ValidateDni(model))
            {
                response.Message="Dni is already used";
                return BadRequest(response);
            }

            var newClient = new Client
            {
                Dni = model.Dni,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone = model.Phone, 
                Address = model.Address,
                City = model.City,
                Province = model.Province,
                PostalCode =model.PostalCode,
                LastModification = DateTime.Now
            };
            clients.Add(newClient);
            var writer = JsonConvert.SerializeObject(clients, Formatting.Indented);
            try
            {
                System.IO.File.WriteAllText(_configuration.GetValue<string>("MySettings:_pathclients"), writer);
                response.Succes = true;
                response.Data = newClient;
                response.Message = "Successfully added";
            }
            catch (Exception)
            {
                response.Message = "Impossible to add";
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new Response();

            var clients = _clientHelper.GetAll();
            if (clients == null)
            {
                response.Message = "File reading failed";
                return BadRequest(response);
            }

            if (clients.Count()==0)
            {
                response.Message = "No Client added yet";
                return NotFound(response);
            }
            response.Succes = true;
            response.Message = "Found successfully";
            response.Data = clients;
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Update(ClientRequest model)
        {
            var response = new Response();

            var clients = _clientHelper.GetAll();
            if (clients == null)
            {
                response.Message = "File reading failed";
                return BadRequest(response);
            }
            var isClientDni = _clientHelper.ValidateDni(model);
            if (!isClientDni)
            {
                response.Message = "Invalid DNI to update";
                return BadRequest(response);
            }
            
            foreach (var client in clients)
            {
                if (client.Dni == model.Dni)
                {
                    client.Dni = model.Dni;
                    client.FirstName = model.FirstName;
                    client.LastName = model.LastName;
                    client.Phone = model.Phone;
                    client.Address = model.Address;
                    client.City = model.City;
                    client.Province = model.Province;
                    client.PostalCode = model.PostalCode;
                    client.LastModification = DateTime.Now;
                    response.Data = client;
                    break;
                }
            }

            try
            {
                var writer = JsonConvert.SerializeObject(clients, Formatting.Indented);
                System.IO.File.WriteAllText(_configuration.GetValue<string>("MySettings:_pathclients"), writer);
                response.Succes = true;
            }
            catch (Exception)
            {
                response.Message = "Impossible to Update";
                return BadRequest(response);
            }
            response.Message = "Successfully updated";
            return Ok(response);
        }

        [HttpDelete("{Dni}")]
        public IActionResult Delete(int Dni)
        {
            var response = new Response();

            var clients = _clientHelper.GetAll();
            if (clients == null)
            {
                response.Message = "File reading failed";
                return BadRequest(response);
            }

            var clientToDelete = clients.FirstOrDefault(client => client.Dni == Dni);
            if (clientToDelete == null)
            {
                response.Message = $"Car with id: {Dni} not found";
                return NotFound(response);
            }

            response.Succes = clients.Remove(clientToDelete);
            try
            {
                var writer = JsonConvert.SerializeObject(clients, Formatting.Indented);
                System.IO.File.WriteAllText(_configuration.GetValue<string>("MySettings:_pathclients"), writer);
            }

            catch (Exception)
            {
                response.Succes = false;
                response.Message = "Impossible to Delete";
                return BadRequest(response);
            }

            response.Data = clientToDelete;
            response.Message = "Delete successfully";
            return Ok(response);
        }
    }
}
