using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace GoNewWebsite.Models
{
    public class ContactUsViewModel
    {
        [JsonProperty("FirstName")]
        [Required(ErrorMessage = "Please enter your full name")]
        [Display(Name = "*Full Name")]
        [StringLength(100, ErrorMessage = "Please Enter Full Name", MinimumLength = 2)]
        public string FullName { get; set; }


        [Display(Name = "*Email Address")]
        [Required(ErrorMessage = "Email address invalid")]
        [EmailAddress(ErrorMessage = "Email address invalid")]
        [StringLength(100, ErrorMessage = "Email Address Invalid", MinimumLength = 5)]
        public string Email { get; set; }


        [Display(Name = "*Phone Number")]
        [Required(ErrorMessage = "Phone number invalid")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(([0]([5|])))\d{7,8}$", ErrorMessage = "Phone number invalid")]
        [Phone(ErrorMessage = "Phone number invalid")]
        [StringLength(10, ErrorMessage = "Phone number invalid", MinimumLength = 10)]
        public string PhoneNumber { get; set; }

    }
}