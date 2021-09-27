using System;
using System.Net;
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
            var rentals = _rentalHelper.GetAll();
            var car = _rentalHelper.GetCar(rentalRequest.Car);
            if (car == null)
            {
                return NotFound(new Response { Message = "Car not found"});
            }

            if (!car.IsFree)
            {
                return BadRequest(new Response { Message = "Car is used" });
            }

            var client = _rentalHelper.GetClient(rentalRequest.Client);
            if (client == null)
            {
                return NotFound(new Response { Message = "Client not found" });
            }

            var rental = new Rental
            {
                Id = Guid.NewGuid(),
                Client = client,
                Car = car,
                StartRental = DateTime.Now,
                RentalDays = rentalRequest.RentalDays
            };
            
            rentals.Add(rental);
            var writer = JsonConvert.SerializeObject(rentals, Formatting.Indented);
            try
            {
                var updateCar = _rentalHelper.UpdateIsFree(rental.Car.Id,false);
                if (updateCar != "")
                {
                    return BadRequest(new Response {Message = updateCar});
                }
                System.IO.File.WriteAllText(_configuration.GetValue<string>("MySettings:_pathrentals"), writer);
                return Ok(new Response{Data = rental,Message = "Successfully added",Succes = true});
            }
            catch
            {
                _rentalHelper.UpdateIsFree(rental.Car.Id, true);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        public IActionResult GetAll()
        {
            var rentals = _rentalHelper.GetAll();
            if (rentals == null)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }

            if (rentals.Count == 0)
            {
                var response = new Response{Message = "List Empty"};
                return NotFound();
            }
            else
            {
                var response = new Response {Data = rentals, Message = "List OK", Succes = true};
                return Ok(response);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var rental = _rentalHelper.GetById(id);
            if (rental == null)
                return BadRequest(new Response {Message = "Rental not found"});
            return Ok(new Response{Data = rental,Message = "Rental found successfuly",Succes = true});
        }
    }
}
