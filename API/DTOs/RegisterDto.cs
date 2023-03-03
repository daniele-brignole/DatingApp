using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.DTOs
{
    public class RegisterDto
    {
        //APIController lega automaticamente i valori parametro alle seguenti proprietà
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string Password { get; set; }    

    }
}