using System;
using System.Linq;
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
    public class CarController : Controller
    {
        private readonly ICarHelper _carHelper;
        private readonly IConfiguration _configuration;

        public CarController(ICarHelper iCarHelper, IConfiguration iconfiguration)
        {
            _carHelper = iCarHelper;
            _configuration = iconfiguration;
        }

        [HttpPost]
        public IActionResult Create(CarRequest model)
        {
            var response = new Response();
            var cars = _carHelper.GetAll();
            if (cars == null)
            {
                response.Message = "File reading failed";
                return BadRequest(response);
            }
            var invalidParamsMessage = _carHelper.ValidateParams(model);
            if (invalidParamsMessage != string.Empty)
            {
                response.Message = invalidParamsMessage;
                return BadRequest(response);
            }
            var car = new Car
            {
                Id = Guid.NewGuid(),
                Transmition = model.Transmition,
                Brand = model.Brand,
                Model = model.Model,
                Doors = model.Doors,
                Color = model.Color,
                IsFree = true
            };
            cars.Add(car);
            var writer = JsonConvert.SerializeObject(cars, Formatting.Indented);
            try 
            {
                System.IO.File.WriteAllText(_configuration.GetValue<string>("MySettings:_pathcars"), writer);
                response.Succes = true;
                response.Data = car;
                response.Message = "Successfully added";
            }
            catch (Exception ex)
            {
                response.Message = "Impossible to add";
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var response = new Response();
            
            var cars = _carHelper.GetAll();
            if (cars == null)
            {
                response.Message = "File reading failed";
                return BadRequest(response);
            }

            response.Data = cars.FirstOrDefault(car => car.Id == id);
            if (response.Data == null)
            {
                response.Message = $"Car with id: {id} not found";
                return NotFound(response);
            }
            response.Succes = true;
            response.Message = "Found successfully";
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Update(CarRequest model)
        {
            var response = new Response();

            var cars = _carHelper.GetAll();
            if (cars == null)
            {
                response.Message = "File reading failed";
                return BadRequest(response);
            }
            var invalidParamsMessage = _carHelper.ValidateParams(model);
            if (invalidParamsMessage != string.Empty)
            {
                response.Message = invalidParamsMessage;
                return BadRequest(response);
            }
            if (!cars.Any(car => car.Id == model.Id))
            {
                response.Message=$"Car with id {model.Id}not found";
                return NotFound(response);
            }

            foreach (var car in cars)
            {
                if (car.Id == model.Id)
                {
                    car.Id = model.Id;
                    car.Transmition = model.Transmition;
                    car.Brand = model.Brand;
                    car.Model = model.Model;
                    car.Doors = model.Doors;
                    car.Color = model.Color;
                    car.IsFree = model.IsFree;
                    response.Data = car;
                    break;
                }
            }
                      
            try
            {
                var writer = JsonConvert.SerializeObject(cars, Formatting.Indented);
                System.IO.File.WriteAllText(_configuration.GetValue<string>("MySettings:_pathcars"), writer);
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

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var response = new Response();

            var cars = _carHelper.GetAll();
            if (cars == null)
            {
                response.Message = "File reading failed";
                return BadRequest(response);
            }

            var carToBeDeleted = cars.FirstOrDefault(car => car.Id == id);
            if (carToBeDeleted == null)
            {
                response.Message = $"Car with id: {id} not found";
                return NotFound(response);
            }

            response.Succes = cars.Remove(carToBeDeleted);
            try
            {
                var writer = JsonConvert.SerializeObject(cars, Formatting.Indented);
                System.IO.File.WriteAllText(_configuration.GetValue<string>("MySettings:_pathcars"), writer);
            }

            catch (Exception)
            {
                response.Succes = false;
                response.Message = "Impossible to Delete";
                return BadRequest(response);
            }
            
            response.Data = carToBeDeleted;
            response.Message = "Delete successfully";
            return Ok(response);
        }
    }
}
