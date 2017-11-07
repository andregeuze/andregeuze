using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyLab2App.Models
{
    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }

        [Range(minimum: 1, maximum: 120)]
        public int Age { get; set; }
    }
}
