using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CursoMVC.Models;
using CursoMVC.Models.TableViewModels;
using Rotativa;

namespace CursoMVC.Controllers
{
    public class PrinterController : Controller
    {
        // GET: Printer
        public ActionResult PrintUsersPDF(string buscar)
        {
            return new ActionAsPdf("InformeUsuarios", new { buscar = buscar })
            {
                FileName = "Probando Informe PDF - Fecha: " + System.DateTime.Now + ".pdf"
            };
        }
        public ActionResult InformeUsuarios(string buscar)
        {
            List<UserTableViewModel> lst = null;
            using (CursoMVCEntities db = new CursoMVCEntities())
            {
                if (!string.IsNullOrEmpty(buscar))
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
    }
}