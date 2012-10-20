using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ValueHelper.WebTest.Controllers
{
    public class TagController : Controller
    {
        //
        // GET: /Tag/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Form(String formName)
        {
            ViewData["formName"] = formName;
            return View();
        }

        [HttpGet]
        public ActionResult Form()
        {
            ViewData["formName"] = "此表单为Get方式传递不了参数";
            return View();
        }
    }
}
