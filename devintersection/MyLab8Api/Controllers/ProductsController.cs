using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyLab8Api.Models;

namespace MyLab8Api.Controllers
{
    [Route("/[controller]")]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private static List<Product> _products = new List<Product>(new[] {
            new Product() { Id = 1, Name = "Computer" },
            new Product() { Id = 2, Name = "Radio" },
            new Product() { Id = 3, Name = "Apple" },
        });
        
        [HttpGet]
        public IEnumerable<Product> Get() => _products;

        [HttpGet("{id}")]
        public Product Get(int id) => _products.SingleOrDefault(p => p.Id == id);


    }
}