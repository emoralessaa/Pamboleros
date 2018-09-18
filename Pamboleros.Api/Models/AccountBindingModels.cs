using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pamboleros.Api.Models
{
    public class CreateUserBindingModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Nombre de usuario")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Apellido")]
        public string LastName { get; set; }

        [Display(Name = "Rol")]
        public string RoleName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El {0} debe tener al menos {2} caracteres de longitud.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar password")]
        [Compare("Password", ErrorMessage = "El password y la confirmación no son iguales.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordBindingModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Actual password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El {0} debe tener al menos {2} caracteres de longitud.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nuevo password")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar nuevo password")]
        [Compare("NewPassword", ErrorMessage = "El nuevo password y la confirmación no son iguales.")]
        public string ConfirmPassword { get; set; }

    }
}