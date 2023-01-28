﻿using HS4_BlogProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace HS4_BlogProject.Application.Models.DTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Must to type UserName")]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Must to type Password")]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Must to type Confirm Password")]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        [DataType(DataType.Password)]
        [Display(Name ="Confirm Password")]
        
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Must to type Email")]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;

    }
}
