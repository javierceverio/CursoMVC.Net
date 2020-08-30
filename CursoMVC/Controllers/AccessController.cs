using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CursoMVC.Models;

namespace CursoMVC.Controllers
{
    public class AccessController : Controller
    {
        // GET: Access
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Enter(string user, string password)
        {
            try
            {
                using(CursoMVCEntities db = new CursoMVCEntities())
                {
                    var lst = from d in db.user
                              where d.email == user && d.password == password && d.id_cstate == 1
                              select d;
                    if (lst.Count() > 0)
                    {
                        user oUser = lst.First();
                        Session["user"] = oUser;
                        return Content("1");
                    }
                    else
                    {
                        return Content("Usuario o Contraseña incorrecto");
                    }
                }
                
            }
            catch (Exception ex)
            {
                return Content("Ocurrio un error :( " + ex.Message);
            }
        }
    }
}