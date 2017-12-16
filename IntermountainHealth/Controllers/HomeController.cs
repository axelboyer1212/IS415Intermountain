using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IntermountainHealth.Models;

namespace IntermountainHealth.Controllers
{
    public class HomeController : Controller
    {
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
                model.Load(id.Value);
            }

            return View("Form", model);
        }

        public ActionResult List()
        {
            var model = new PatientListModel();
            return View("List", model);
        }
    }
}