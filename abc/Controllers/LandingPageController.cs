using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using abc.Models;
using abc.DataAccess;

namespace abc.Controllers
{
    public class LandingPageController : Controller
    {
        //
        // GET: /LandingPage/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SaveFeedback(FeedBack fb)
        {
            DatabaseQueries dq = new DatabaseQueries();
            dq.SaveFeedback(fb);
           
            return RedirectToAction("Index"); 
        }
	}
}