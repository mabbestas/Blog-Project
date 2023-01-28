﻿using HS4_BlogProject.Application.Extensions;
using HS4_BlogProject.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS4_BlogProject.Application.Models.DTOs
{
    public class UpdateAuthorDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Must to type First Name")]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Must to type Last Name")]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        public string LastName { get; set; }
        public string ImagePath { get; set; }

        // custom Extension yazıcaz. Custom Data Annotation yazıcaz. jpeg, png uzantılı dosyalar sadece yüklensin
        [PictureFileExtension]
        public IFormFile UploadPath { get; set; }
        public DateTime UpdateDate => DateTime.Now;
        public Status Status => Status.Modified;
    }
}
