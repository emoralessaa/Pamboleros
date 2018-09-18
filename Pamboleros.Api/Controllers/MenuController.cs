using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Pamboleros.Api.Infrastructure;
using Pamboleros.Api.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Pamboleros.Api.Controllers
{
    [RoutePrefix("api/menu")]
    public class MenuController : BaseApiController
    {
        private ApplicationDbContext _dbContext;

        // GET api/Menu       
        [AcceptVerbs("POST")]
        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        [Route("create")]
        //public async Task<IHttpActionResult> CreateMenu(SideMenu sideMenu)
        public async Task<IHttpActionResult> CreateMenu([FromUri] string MenuName, [FromUri] string MenuHref,
            [FromUri] int MenuLevel, [FromUri] Guid MenuIdRoot, [FromUri] string MenuIcon, [FromUri] bool MenuChilds)
        {
            Menu menu = new Menu()
            {
                MenuId= Guid.NewGuid(),
                MenuName = MenuName,
                MenuHref = MenuHref,
                MenuLevel = MenuLevel,
                MenuIdRoot = MenuIdRoot,
                MenuIcon = MenuIcon,
                MenuChilds= MenuChilds,
                MenuStat = true
            };

            using (_dbContext = new ApplicationDbContext())
            {
                _dbContext.AddMenu(menu);
                Uri locationHeader = new Uri(Url.Link("GetMenuById", new { id = menu.MenuId }));
                _dbContext.SaveChanges();
                return Created(locationHeader, menu);
            }
        }

        // ----------- PRIMEROS MENUS AUTOMATICOS ----------- //
        [AllowAnonymous]
        [HttpPost]
        [Route("createFirstMenu")]
        public async Task<IHttpActionResult> createFirstMenu()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            Guid menuIdHome = Guid.NewGuid();
            Guid menuIdConfig = Guid.NewGuid();
            Guid menuIdPages = Guid.NewGuid();

            Menu[] menus = new Menu[]
            {
                new Menu() { MenuId = menuIdHome, MenuName = "Home", MenuDisplay="Inicio", MenuHref = "Home",MenuLevel = 0,/*MenuIdRoot =,*/MenuStat = true,/*MenuIcon=,*/ MenuChilds = false },
                new Menu() { MenuId = menuIdConfig, MenuName = "Config", MenuDisplay="Configuración", MenuHref = "Config",MenuLevel = 0,/*MenuIdRoot =,*/MenuStat = true,/*MenuIcon=,*/ MenuChilds = true },
                new Menu() { MenuId = menuIdPages, MenuName = "Pages", MenuDisplay = "Páginas", MenuHref = "Pages", MenuLevel = 1, MenuIdRoot = menuIdConfig, MenuStat = true,/*MenuIcon=,*/ MenuChilds = false }
        };
            using (_dbContext = new ApplicationDbContext())
            {
                _dbContext.AddMenu(menus);
                
                var adminRole = roleManager.FindByName("SuperAdmin");
                MenuRol[] menusRoles = new MenuRol[]
                {
                new MenuRol() { MenuId = menuIdHome, RoleId = Guid.Parse(adminRole.Id), MenuStat = true },
                new MenuRol() { MenuId = menuIdConfig, RoleId = Guid.Parse(adminRole.Id), MenuStat = true },
                new MenuRol() { MenuId = menuIdPages, RoleId = Guid.Parse(adminRole.Id), MenuStat = true }
                };
                _dbContext.AddMenuRol(menusRoles);
                Uri locationHeader = new Uri(Url.Link("getMenuByRol", new { id = adminRole.Id }));
                await _dbContext.CommitChanges();
                return Created(locationHeader, menus);
            }
        }
        // ----------- FIN PRIMEROS MENUS AUTOMATICOS ----------- //


        [Authorize(Roles = "SuperAdmin")]
        [Route("getMenu", Name = "GetMenuById")]
        public async Task<IHttpActionResult> GetMenu(Guid MenuId)
        {
            using (_dbContext = new ApplicationDbContext())
            {
                var menu = _dbContext.GetMenu(MenuId);

                if (menu != null)
                {
                    return Ok(menu);
                }
            }

            return NotFound();
        }

        [Authorize(Roles = "SuperAdmin")]
        [Route("menu/roles")]
        [HttpPost]
        public async Task<IHttpActionResult> AssignMenuToRoles([FromUri] Guid MenuId, [FromUri] Guid RoleId)
        {
            using (_dbContext = new ApplicationDbContext())
            {
                var menu = _dbContext.GetMenu(MenuId);

                if (menu == null)
                {
                    ModelState.AddModelError("", "El menú no existe.");
                    return BadRequest(ModelState);
                }

                var role = await this.AppRoleManager.FindByIdAsync(RoleId.ToString());

                if (role == null)
                {
                    ModelState.AddModelError("", "El rol no existe.");
                    return BadRequest(ModelState);
                }

                var currentMenuRole = _dbContext.GetMenuRol(menu.MenuId, Guid.Parse(role.Id));
                if (currentMenuRole == null)
                {
                    MenuRol menuRol = new MenuRol();
                    menuRol.MenuId = menu.MenuId;
                    menuRol.RoleId = Guid.Parse(role.Id);
                    menuRol.MenuStat = true;

                    _dbContext.AddMenuRol(menuRol);
                    Uri locationHeader = new Uri(Url.Link("GetMenuRol", new { menu = menuRol.MenuId, rol = menuRol.RoleId }));
                    _dbContext.SaveChanges();
                    return Created(locationHeader, menuRol);
                }
                else
                {
                    ModelState.AddModelError("", "La asignación ya existe.");
                    return BadRequest(ModelState);
                }
            }
        }

        [Authorize(Roles = "SuperAdmin")]
        [Route("getMenuByRol", Name = "getMenuByRol")]
        public IHttpActionResult GetMenuByRol([FromUri] Guid RoleId)
        {
            using (_dbContext = new ApplicationDbContext())
            {
                return Ok(_dbContext.GetMenuByRol(RoleId).ToList());
            }
        }

        [Authorize(Roles = "SuperAdmin")]
        [Route("getMenuRol", Name = "GetMenuRol")]
        public async Task<IHttpActionResult> GetMenuRol(Guid MenuId, Guid RoleId)
        {
            using (_dbContext = new ApplicationDbContext())
            {
                var menu = _dbContext.GetMenuRol(MenuId, RoleId);

                if (menu != null)
                {
                    return Ok(menu);
                }
            }

            return NotFound();
        }
    }
}