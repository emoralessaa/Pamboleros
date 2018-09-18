using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pamboleros.Api.Models
{
    [Table("Equipos")]
    public class Equipo
    {
        [Key]
        public Guid EquipoID { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "El {0} debe contener por lo menos {2} caracteres de longitud.", MinimumLength = 2)]
        [Display(Name = "Nombre del equipo")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "El {0} debe contener por lo menos {2} caracteres de longitud.", MinimumLength = 2)]
        [Display(Name = "Categoría en la que juega")]
        public string Categoria { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El {0} debe contener por lo menos {2} caracteres de longitud.", MinimumLength = 2)]
        [Display(Name = "Delegado o responsable del equipo")]
        public string Delegado { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "El {0} debe contener por lo menos {6} caracteres de longitud.", MinimumLength = 6)]
        [Display(Name = "Teléfono de contacto")]
        public string TelDelegado { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El {0} debe contener por lo menos {6} caracteres de longitud.", MinimumLength = 6)]
        [Display(Name = "Correo de contacto")]
        public string MailDelegado { get; set; }

        public IList<Jugador> Jugadores;
    }

    [Table("Jugadores")]
    public class Jugador
    {
        [Key]
        public Guid JugadorID { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "El {0} debe contener por lo menos {2} caracteres de longitud.", MinimumLength = 2)]
        [Display(Name = "Nombre del jugador")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "El {0} debe contener por lo menos {2} caracteres de longitud.", MinimumLength = 2)]
        [Display(Name = "Apellido paterno")]
        public string ApellidoP { get; set; }

        [Display(Name = "Apellido materno")]
        public string ApellidoM { get; set; }

        [Display(Name = "Edad")]
        public int Edad { get; set; }

        [Display(Name = "Teléfono de contacto")]
        public string Telefono { get; set; }

        [Display(Name = "Teléfono de emergencia")]
        public string TelEmergencia { get; set; }

        [Display(Name = "Contacto de emergencia")]
        public string ContactoEmergencia { get; set; }

        [Display(Name = "Comentarios")]
        public string Comentarios { get; set; }
    }
}