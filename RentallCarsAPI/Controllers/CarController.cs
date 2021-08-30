using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentallCarsAPI.Models.Response;
using RentallCarsAPI.Models;
using RentallCarsAPI.Models.Request;
using Newtonsoft.Json;

namespace RentallCarsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : Controller
    {
        [HttpPost]
        public IActionResult Create(CarRequest model)
        {
            var oResponse = new Response();
            try
            {
                var cars = GetAll();
                foreach (var aux in cars) 
                {
                    if (model.Id == aux.Id)
                    {
                        oResponse.Message = "Invalid Id, it´s used";
                        return Ok(oResponse);
                    }
                }
                if (((int)model.Mark >= 0 && (int)model.Mark <= 5) && ((int)model.Transmition >= 0 && (int)model.Transmition <= 1))
                {
                    var oCar = new Car();
                    oCar.Id = model.Id;
                    oCar.Transmition = model.Transmition;
                    oCar.Mark = model.Mark;
                    oCar.Model = model.Model;
                    oCar.Doors = model.Doors;
                    oCar.Color = model.Color;
                    cars.Add(oCar);
                    var writer = JsonConvert.SerializeObject(cars,Formatting.Indented);
                    System.IO.File.WriteAllText("cars.txt", writer);
                    oResponse.Succes = true;
                    oResponse.Data = writer;
                }
                else 
                {
                    oResponse.Message = "Invalid Transmition or Mark";
                }
            }
            catch(Exception ex)
            {
                oResponse.Message = "Impossible to add";
            }

            return Ok(oResponse);

        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var oResponse = new Response();

            try
            {
                var cars = GetAll();
                foreach (var aux in cars)
                {
                    if (aux.Id == id)
                    {
                        oResponse.Succes = true;
                        oResponse.Data = aux;
                        oResponse.Message = "Found successfully";
                        return Ok(oResponse);
                    }
                }
                
            }
            catch (Exception ex)
            {
                oResponse.Message = "Serch error";
            }

            return Ok(oResponse);
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
                
            }
            return cars;
        }
    }
}
