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
            if (!ValidateParams(model))
            {
                response.Message = "Invalid Transmition or Mark";
                return BadRequest(response);
            }
            var car = new Car();
            car.Id = ++cars.Last().Id;
            car.Transmition = model.Transmition;
            car.Mark = model.Mark;
            car.Model = model.Model;
            car.Doors = model.Doors;
            car.Color = model.Color;
            cars.Add(car);
            try
            {
                var writer = JsonConvert.SerializeObject(cars, Formatting.Indented);
                System.IO.File.WriteAllText("cars.txt", writer);
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
        public IActionResult Get(int id)
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
                    response.Succes = true;
                    response.Data = aux;
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
            if (!ValidateParams(model))
            {
                response.Message = "Invalid Transmition or Mark";
                return BadRequest(response);
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
                System.IO.File.WriteAllText("cars.txt", writer);
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
        public IActionResult Delete(int id)
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
                        System.IO.File.WriteAllText("cars.txt", writer);
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

        private bool ValidateParams(CarRequest model)
        {
            bool flag = false;
            if ((int)model.Mark >= 0 && (int)model.Mark <= 5)
            {
                if((int)model.Transmition >= 0 && (int)model.Transmition <= 1)
                {
                    flag = true;
                }                    
            }    
            return flag;
        }
        private List<Car> GetAll() {
            var cars = new List<Car>();
            try
            {
                string list = System.IO.File.ReadAllText("cars.txt");
                cars = JsonConvert.DeserializeObject<List<Car>>(list);
            }
            catch (Exception ex)
            {
                return null;
            }
            return cars;
        }
    }
}
