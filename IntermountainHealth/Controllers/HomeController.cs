using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IntermountainHealth.DAL;
using IntermountainHealth.Models;

namespace IntermountainHealth.Controllers
{
    public class HomeController : Controller
    {
        private DataContext db = new DataContext();

        public ActionResult Index()
        {
            return RedirectToAction("Form");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Form(int? id)
        {
            var model = new PatientFormModel();

            if (id != null)
            {
                var patient = db.patients.Find(id);
                if (patient == null)
                {
                    int? nullid = null;
                    return RedirectToAction("Form", new { id = nullid });
                }

                model.Load(id.Value);
                if (model.idFound)
                {
                    model.IsEdit = true;
                }
                else
                {
                    model.IsEdit = false;
                }

            }
            else
            {
                model.IsEdit = false;
            }
            return View("Form", model);
        }

        [HttpPost]
        public ActionResult Form(PatientFormModel model)
        {
            if (ModelState.IsValid)
            {
                model.Save();
            }
            else
            {
                return View(model);
            }
            int id = model.Id;

            return RedirectToAction("form", new { id = id });
        }

        public ActionResult List()
        {
            var model = new PatientListModel();
            model.LoadItems();
            return View("List", model);
        }

        public ActionResult Delete(int id)
        {
            var patient = db.patients.Find(id);
            if (patient != null)
            {
                db.patients.Remove(patient);
                db.SaveChanges();
            }

            return RedirectToAction("List");
        }
        public ActionResult Tableau()
        {
            return View();
        }

    }
}