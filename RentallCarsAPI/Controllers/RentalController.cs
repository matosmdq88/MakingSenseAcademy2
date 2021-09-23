using System;
using Microsoft.AspNetCore.Mvc;
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
    public class RentalController : Controller
    {
        private readonly IRentalHelper _rentalHelper;
        private readonly IConfiguration _configuration;

        public RentalController(IRentalHelper iRentalHelper,IConfiguration iConfiguration)
        {
            _configuration = iConfiguration;
            _rentalHelper = iRentalHelper;
        }

        [HttpPost]
        public IActionResult Create(RentalRequest rentalRequest)
        {
            var response = new Response();
            var rentals = _rentalHelper.GetAll();
            var car = _rentalHelper.GetCar(rentalRequest.Car);
            if (car == null)
            {
                response.Message = "Car not found";
                return NotFound(response);
            }

            if (!car.IsFree)
            {
                response.Message = "Car is used";
                return BadRequest(response);
            }

            var client = _rentalHelper.GetClient(rentalRequest.Client);
            if (client == null)
            {
                response.Message = "Client not found";
                return NotFound(response);
            }

            var rental = new Rental
            {
                Id = Guid.NewGuid(),
                Client = client,
                Car=car,
                StartRental = DateTime.Now,
                RentalDays = rentalRequest.RentalDays
            };
            
            rentals.Add(rental);
            var writer = JsonConvert.SerializeObject(rentals, Formatting.Indented);
            try
            {
                System.IO.File.WriteAllText(_configuration.GetValue<string>("MySettings:_pathrentals"), writer);
                response.Succes = true;
                response.Data = rental;
                response.Message = "Successfully added";
            }
            catch
            {
                response.Message = "Imposible to add";
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
