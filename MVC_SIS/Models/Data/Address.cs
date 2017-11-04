using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Exercises.Models.Data
{
    public class Address
    {        
        public int AddressId { get; set; }
        [Required(ErrorMessage = "Please enter street 1.")]
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        [Required(ErrorMessage = "Please enter city.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please enter state.")]
        public State State { get; set; }
        [Required(ErrorMessage = "Please enter postal code.")]
        public string PostalCode { get; set; }
    }
}