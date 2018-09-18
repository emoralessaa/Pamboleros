using Microsoft.AspNet.Identity.EntityFramework;
using Pamboleros.Api.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Pamboleros.Api.Infrastructure
{
    public interface IApplicationDbContext
    {
        #region Menu
        DbSet<Menu> Menus { get; set; }

        Menu GetMenu(Guid MenuId);

        Menu GetMenu(string MenuName);

        IEnumerable<Menu> GetMenu();

        void AddMenu(Menu menu);

        void AddMenu(Menu[] menus);

        DbSet<MenuRol> MenusRoles { get; set; }

        MenuRol GetMenuRol(Guid MenuId, Guid RoleId);

        void AddMenuRol(MenuRol menuRol);

        void AddMenuRol(MenuRol[] menuRols);

        IEnumerable<Menu> GetMenuByRol(Guid RoleId);
        #endregion

        #region Equipos
        DbSet<Equipo> Equipos { get; set; }

        DbSet<Jugador> Jugadores { get; set; }

        #endregion

        Task CommitChanges();
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext()
        : base("PambolerosDB", throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        #region Menu
        // ------------ Menu ------------ //
        public virtual DbSet<Menu> Menus { get; set; }

        public Menu GetMenu(Guid MenuId)
        {
            return Menus.SingleOrDefault(t => t.MenuId == MenuId);
        }
        public Menu GetMenu(string MenuName)
        {
            return Menus.SingleOrDefault(t => t.MenuName == MenuName);
        }

        public IEnumerable<Menu> GetMenu()
        {
            return Menus.Where(t => t.MenuStat == true);
        }

        public void AddMenu(Menu menu)
        {
            Menus.Add(menu);
        }

        public void AddMenu(Menu[] menus)
        {
            foreach (Menu menu in menus)
            {
                Menus.Add(menu);
            }
        }

        // ------------ MenuRol ------------ //
        public virtual DbSet<MenuRol> MenusRoles { get; set; }

        public MenuRol GetMenuRol(Guid MenuId, Guid RoleId)
        {
            return MenusRoles.FirstOrDefault(t => t.MenuId == MenuId && t.RoleId == RoleId);
        }

        public void AddMenuRol(MenuRol menuRol)
        {
            MenusRoles.Add(menuRol);
        }

        public void AddMenuRol(MenuRol[] menuRols)
        {
            foreach (MenuRol menuRol in menuRols)
            {
                MenusRoles.Add(menuRol);
            }
        }

        public IEnumerable<Menu> GetMenuByRol(Guid RoleId)
        {
            var sm_roles = MenusRoles.Where(t => t.RoleId == RoleId).Join(Menus, t => t.MenuId, sm => sm.MenuId, (t, sm) => new { t, sm });
            return sm_roles.Select(t => t.sm).OrderBy(t => t.MenuIdRoot).OrderBy(t => t.MenuLevel);
        }
        #endregion

        #region Equipos
        public virtual DbSet<Equipo> Equipos { get; set; }

        public virtual DbSet<Jugador> Jugadores { get; set; }

        #endregion

        public async Task CommitChanges()
        {
            await SaveChangesAsync();
        }
    }
}