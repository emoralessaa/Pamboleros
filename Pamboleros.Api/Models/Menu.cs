using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pamboleros.Api.Models
{
    [Table("Menu")]
    public class Menu
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public SideMenu()
        //{
        //    SideMenus_Roles = new HashSet<SideMenu_Rol>();
        //}

        [Key]
        public Guid MenuId { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "El {0} debe contener por lo menos {2} caracteres de longitud.", MinimumLength = 2)]
        [Display(Name = "Nombre del menu")]
        public string MenuName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "El {0} debe contener por lo menos {2} caracteres de longitud.", MinimumLength = 2)]
        [Display(Name = "Nombre del menu que aparecerá en pantalla")]
        public string MenuDisplay { get; set; }

        public string MenuHref { get; set; }

        [Required]
        [Display(Name = "Nivel del menu")]
        public int MenuLevel { get; set; }

        [Display(Name = "Nodo padre del menu")]
        public Guid MenuIdRoot { get; set; }

        [Display(Name = "Estatus del menu")]
        public bool MenuStat { get; set; }

        [Display(Name = "Icono del menu")]
        public string MenuIcon { get; set; }

        [Display(Name = "Tiene hijos?")]
        public bool MenuChilds { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<SideMenu_Rol> SideMenus_Roles { get; set; }
    }
}