using System;
using System.ComponentModel.DataAnnotations;

namespace VueJsDemo.Api.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string FirstName { get; set; }
        [MaxLength(255)]
        public string LastName { get; set; }
        [MaxLength(255)]
        public string Company { get; set; }
        [MaxLength(255)]
        public string JobTitle { get; set; }
        [MaxLength(255)]
        public string Email { get; set; }
        [MaxLength(20)]
        public string MobilePhone { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}