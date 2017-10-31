using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalManagement.ViewModels.Account
{
    public class SignUpViewModel
    {

        

        [Required(ErrorMessage = "Please enter your First name")]
        [Display(Name = "Fname")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Characters are not allowed.")]
        public string Fname { get; set; }

        [Required(ErrorMessage = "Please enter your Last name")]
        [Display(Name = "Lname")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Characters are not allowed.")]
        public string Lname { get; set; }
        
        [Required(ErrorMessage = "Please enter your Email Address")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
  

        [Required(ErrorMessage = "Please enter your password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The password must be atleat 6 characters long.", MinimumLength = 6)]
        [Display(Name = "LoginPassword")]
        public string LoginPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("LoginPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string LoginPasswordConf { get; set; }

        [Required(ErrorMessage = "Please select your gender")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Please enter your date of birth")]
        [DataType(DataType.Date)]
        [Display(Name = "Birthday")]
        public string Birthday { get; set; }

        [Required(ErrorMessage = "Please enter your country")]
        [Display(Name = "Country")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Characters are not allowed.")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Please enter your city")]
        [Display(Name = "City")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Characters are not allowed.")]
        public string City { get; set; }


        [Required(ErrorMessage = "Please enter your contact no")]
        [Display(Name = "ContactNo")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Characters are not allowed.")]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "Please enter your address")]
        [Display(Name = "Address")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Display(Name = "About")]
        [DataType(DataType.MultilineText)]
        public string About { get; set; }

        [Required(ErrorMessage = "Please select your type")]
        [Display(Name = "Type")]
        public string Type { get; set; }

    }
}
