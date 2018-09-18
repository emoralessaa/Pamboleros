using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pamboleros.Api.Models
{
    [Table("MenuRol")]
    public class MenuRol
    {
        [Key]
        [Required]
        [Column("MenuId", Order = 0)]
        public Guid MenuId { get; set; }

        [Key]
        [Required]
        [Column("RoleId", Order = 1)]
        public Guid RoleId { get; set; }

        [Required]
        [Display(Name = "Estatus del menu")]
        public bool MenuStat { get; set; }

        //public virtual SideMenu SideMenu { get; set; }
    }
}