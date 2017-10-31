using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalManagement.ViewModels.Admin
{
    public class PatientViewModel
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
        

        [Required(ErrorMessage = "Please select your gender")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Please enter your age")]
        [Display(Name = "PatientAge")]
        public int PatientAge { get; set; }
      

        [Required(ErrorMessage = "Please select your blood group")]
        [Display(Name = "BloodGroup")]
        public string BloodGroup { get; set; }

        [Display(Name = "ContactNo")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Characters are not allowed.")]
        public string ContactNo { get; set; }


        [Display(Name = "Address")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }


        [Display(Name = "PatientDetails")]
        [DataType(DataType.MultilineText)]
        public string PatientDetails { get; set; }

        [Display(Name = "PatientHistory")]
        [DataType(DataType.MultilineText)]
        public string PatientHistory { get; set; }


        [Display(Name = "DoctorDiagnosis")]
        [DataType(DataType.MultilineText)]
        public string DoctorDiagnosis { get; set; }

        [Display(Name = "Comments")]
        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }

        [Display(Name = "ProfilePicture")]
        public string ProfilePicture { get; set; }



    }
}
