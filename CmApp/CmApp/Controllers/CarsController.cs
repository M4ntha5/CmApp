﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmApp.Entities;
using CmApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CmApp.Controllers
{
    [Route("/api/cars")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        // GET: api/Cars
       [HttpGet]
        public IEnumerable<string> Get()
        {
            var repo = new CarRepository();             //veikiantis ex su codemash

            var cars = repo.test().Result;

            //return cars;
            return new string[] { "value11", "value21" };
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Cars
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Cars/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
