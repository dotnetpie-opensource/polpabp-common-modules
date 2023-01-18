using System;
using System.ComponentModel.DataAnnotations;

namespace PolpAbp.Contact.Dtos
{
    public class ContactCardInputDto
    {
        [Required]
        [MinLength(1)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(1)]
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneCountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneConfirmed { get; set; }
    }
}

