using System;
using System.ComponentModel.DataAnnotations;

namespace DiplomProject.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "")]
        public string FullName { get; set; } = string.Empty;

        [Phone(ErrorMessage = "")]
        public string Phone { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "")]
        public string Email { get; set; } = string.Empty;

        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}
