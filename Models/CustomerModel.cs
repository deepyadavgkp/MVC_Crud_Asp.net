using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Demo.Models
{
    public class CustomerModel
    {
        [Key]

        public int ID { get; set; }



        [Required(ErrorMessage = "Enter First Name")]

        public string Firstname { get; set; }



        [Required(ErrorMessage = "Enter Lat Name")]

        public string Lastname { get; set; }



        [Required(ErrorMessage = "Enter Mobileno")]

        public int Age { get; set; }



        [DataType(DataType.Date)]

        [Required(ErrorMessage = "Enter Birthdate")]

        public DateTime Birthdate { get; set; }

        public string State { get; set; }
        public string Gender { get; set; }

        public string Photo { get; set; }




    }
}