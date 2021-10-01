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
            if (_rentalHelper.SaveRental(rentals) != "")
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            var response = new Response { Data = rental, Message = "Successfully saved", Succes = true };
            return Ok(response);
        }

        [HttpGet]
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

        [HttpPut("{id}")]
        public IActionResult EndRental(Guid id)
        {
            var rental = _rentalHelper.GetById(id);
            if (rental == null)
            {
                return NotFound(new Response {Message = "Rental not found"});
            }
            rental.FinishRental=DateTime.Now;
            rental.Penalisation = (rental.FinishRental.Day - rental.StartRental.Day - rental.RentalDays)*2000;
            var rentals = _rentalHelper.GetAll();
            foreach (var rentCar in rentals)
            {
                if (rentCar.Id == rental.Id)
                {
                    rentCar.Penalisation = rental.Penalisation;
                    rentCar.FinishRental = rental.FinishRental;
                    break;
                }
            }
            if (_rentalHelper.SaveRental(rentals) != "")
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            var response = new Response { Data = rental, Message = "Successfully saved",Succes = true};
            return Ok(response);
        }
    }
}
