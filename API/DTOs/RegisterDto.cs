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
        public string Password { get; set; }    

    }
}