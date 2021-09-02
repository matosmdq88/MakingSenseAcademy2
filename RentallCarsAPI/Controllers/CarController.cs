using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentallCarsAPI.Models;
using RentallCarsAPI.Models.Request;
using RentallCarsAPI.Models.Response;

namespace RentallCarsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : Controller
    {
        private readonly 
        private const string _path = "cars.txt";
        [HttpPost]
        public IActionResult Create(CarRequest model)
        {
            var response = new Response();           
            var cars = GetAll();
            if (cars == null)
            {
                response.Message = "File reading failed";
                return BadRequest(response);
            }
            var invalidParamsMessage = ValidateParams(model);
            if (invalidParamsMessage != string.Empty)
            {
                response.Message = invalidParamsMessage;
                return BadRequest(response);
            }
            var car = new Car
            {
                Id = Guid.NewGuid(),
                Transmition = model.Transmition,
                Mark = model.Mark,
                Model = model.Model,
                Doors = model.Doors,
                Color = model.Color,
            };
            cars.Add(car);
            var writer = JsonConvert.SerializeObject(cars, Formatting.Indented);
            try 
            {
                System.IO.File.WriteAllText(_path, writer);
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
            
            var cars = GetAll();
            if (cars == null)
            {
                response.Message = "File reading failed";
                return BadRequest(response);
            }
            foreach (var car in cars)
            {
                if (car.Id == id)
                {
                    response.Succes = true;
                    response.Data = car;
                    response.Message = "Found successfully";
                    return Ok(response);
                }
            }
            response.Message = $"Car with id: {id} not found";
            return NotFound(response);
        }

        [HttpPut]
        public IActionResult Update(CarRequest model)
        {
            var response = new Response();

            var cars = GetAll();
            if (cars == null)
            {
                response.Message = "File reading failed";
                return BadRequest(response);
            }
            var invalidParamsMessage = ValidateParams(model);
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
                    car.Mark = model.Mark;
                    car.Model = model.Model;
                    car.Doors = model.Doors;
                    car.Color = model.Color;
                    response.Data = car;
                    break;
                }
            }
                      
            try
            {
                var writer = JsonConvert.SerializeObject(cars, Formatting.Indented);
                System.IO.File.WriteAllText(_path, writer);
                response.Succes = true;
            }
            catch (Exception ex)
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

            var cars = GetAll();
            if (cars == null)
            {
                response.Message = "File reading failed";
                return BadRequest(response);
            }
            foreach (var aux in cars)
            {
                if (aux.Id == id)
                {
                    cars.Remove(aux);
                    try
                    {
                        var writer = JsonConvert.SerializeObject(cars, Formatting.Indented);
                        System.IO.File.WriteAllText(_path, writer);
                        response.Succes = true;
                    }
                    catch (Exception ex)
                    {
                        response.Message = "Impossible to Delete";
                        return BadRequest(response);
                    }
                    response.Succes = true;
                    response.Data = aux;
                    response.Message = "Delete successfully";
                    return Ok(response);
                }
            }
            response.Message = $"Car with id: {id} not found";
            return NotFound(response);
        }

        private string ValidateParams(CarRequest model)
        {
            if (!Enum.IsDefined(typeof(EnumTransmition), model.Transmition))
            {
                return "Invalid transmition";
            }
            if (!Enum.IsDefined(typeof(EnumMark), model.Mark))
            {
                return "Invalid mark";
            }
            return string.Empty;
        }
        private List<Car> GetAll()
        {
            var cars = new List<Car>();
            if (!System.IO.File.Exists(_path))
            {
                return cars;
            }
            try
            {
                var list = System.IO.File.ReadAllText(_path);
                if (list == "")
                    return cars;
                else
                    return JsonConvert.DeserializeObject<List<Car>>(list); 
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
