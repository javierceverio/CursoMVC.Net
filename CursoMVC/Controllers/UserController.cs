using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CursoMVC.Models;
using CursoMVC.Models.TableViewModels;
using CursoMVC.Models.ViewModels;
using Rotativa;

namespace CursoMVC.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index(string buscar)
        {
            List <UserTableViewModel> lst = null;
            using (CursoMVCEntities db = new CursoMVCEntities())
            {
                if(!string.IsNullOrEmpty(buscar))
                {
                    lst = (from d in db.user
                           where d.id_cstate == 1 && d.email.StartsWith(buscar)
                           orderby d.email
                           select new UserTableViewModel
                           {
                               Email = d.email,
                               Id = d.id,
                               Edad = d.edad

                           }).ToList();
                }
                else
                {
                    lst = (from d in db.user
                           where d.id_cstate == 1
                           orderby d.email
                           select new UserTableViewModel
                           {
                               Email = d.email,
                               Id = d.id,
                               Edad = d.edad

                           }).ToList();
                }
            }
            return View(lst);
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (var db = new CursoMVCEntities())
            {
                user oUser = new user();
                oUser.id_cstate = 1;
                oUser.email = model.Email;
                oUser.password = model.Password;
                oUser.edad = model.Edad;

                db.user.Add(oUser);
                db.SaveChanges();
            }
            return Redirect(Url.Content("~/User"));
        }

        public ActionResult Edit(int Id)
        {
            EditUserViewModel model = new EditUserViewModel();
            using (var db = new CursoMVCEntities()) {
                var oUser = db.user.Find(Id);
                model.Email = oUser.email;
                model.Edad = (int)oUser.edad;
                model.Id = oUser.id;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (var db = new CursoMVCEntities())
            {
                var oUser = db.user.Find(model.Id);
                oUser.email = model.Email;
                oUser.edad = model.Edad;
                if(model.Password != null && model.Password.Trim() != "")
                {
                    oUser.password = model.Password;
                }

                db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return Redirect(Url.Content("~/User"));
        }
        /*public ActionResult Delete(int Id)
        {
            DeleteUserViewModel model = new DeleteUserViewModel();
            using (var db = new CursoMVCEntities())
            {
                var oUser = db.user.Find(Id);
                model.Email = oUser.email;
                model.Edad = (int)oUser.edad;
                model.Id = oUser.id;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(EditUserViewModel model)
        {
            using (var db = new CursoMVCEntities())
            {
                var oUser = db.user.Find(model.Id);
                oUser.id_cstate = 3;

                db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return Redirect(Url.Content("~/User"));
        }*/
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            using (var db = new CursoMVCEntities())
            {
                var oUser = db.user.Find(Id);
                oUser.id_cstate = 3; // Lo ponemos en el estado "Eliminado"

                db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return Content("1");
        }
    }
}