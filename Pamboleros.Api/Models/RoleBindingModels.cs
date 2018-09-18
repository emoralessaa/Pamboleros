using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pamboleros.Api.Models
{
    public class CreateRoleBindingModel
    {
        [Required]
        [StringLength(256, ErrorMessage = "El {0} debe contener por lo menos {2} caracteres de longitud.", MinimumLength = 2)]
        [Display(Name = "Nombre del rol")]
        public string Name { get; set; }

    }

    public class UsersInRoleModel
    {

        public string Id { get; set; }
        public List<string> EnrolledUsers { get; set; }
        public List<string> RemovedUsers { get; set; }
    }
}