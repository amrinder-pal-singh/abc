using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; 
using abc.Models;
using abc.DataAccess;
using abc.ViewModels;
namespace abc.Controllers
{
    public class RecruitmentZoneController : Controller
    {
        //


        public ActionResult temp()
        {
            return View("temp");

        }


        // GET: /RecruitmentZon
        public ActionResult Login()
        {
            Session["RegistrationId"] = null;

            return View("Login",new UserRegistration());
        }
      [HttpPost]
        public ActionResult AfterUserLogin(UserRegistration ur)
        {
            DatabaseQueries obj = new DatabaseQueries();
            if (obj.CheckValidUser(ur))
            {
                ur=obj.GetUserByUserName(ur.UserRegistration_UserName);
                Session["RegistrationId"] = ur.UserRegisteration_RegisterationId;
                return RedirectToAction("HomePage");
                
            }
              
            
          
          return RedirectToAction("Login");
        }
      public ActionResult HomePage(UserRegistration ur)
      {
         int id = Convert.ToInt32(Session["RegistrationId"]);
 
          
          return View("HomePage",ur);
      }
        public ActionResult Register()
        {
           
            
            return View("Register",new UserRegistration()); 
        }
        [HttpPost]
        public ActionResult Register(UserRegistration ur)
        {
            DatabaseQueries obj = new DatabaseQueries();
            obj.Register(ur);
           return RedirectToAction("Login");
        }
      
      
        #region Personal Information
        public ActionResult ShowPersonalInformation()
        {
            int Id = Convert.ToInt32(Session["RegistrationId"]);
            DatabaseQueries obj = new DatabaseQueries();
            PersonalInformation pi = new PersonalInformation();
            ViewBag.GetPersonalInformationByRegistrationId = obj.GetPersonalInformationByRegistrationId(Id);
           
        
          
            return View("ShowPersonalInformation",pi);  
        }
        public ActionResult EditPersonalInformation(FormCollection form)
        {
            int Id = Convert.ToInt32(form["PersonalInformation_Id"].ToString());
            DatabaseQueries obj = new DatabaseQueries();
            PersonalInformation pi = new PersonalInformation();

            pi = obj.GetPersonalInformationByPersonalInformationId(Id);
                      
       
            return View("EditPersonalInformation",pi); 
        }
        [HttpPost]
        public ActionResult SavePersonalInformation(PersonalInformation pf,HttpPostedFileBase file)
        {
           

            pf.PersonalInformation_RegistrationId = Convert.ToInt32(Session["RegistrationId"]);
            DatabaseQueries obj = new DatabaseQueries();
            if (pf.PersonalInformation_Id == 0)
            {
                //string ImageName = System.IO.Path.GetFileName(file.FileName);
                //string UserImages = (pf.PersonalInformation_RegistrationId + ImageName);
                //string PhysicalPath = Server.MapPath("~/Content/UserImages" + UserImages);
                //file.SaveAs(PhysicalPath);
                //pf.PersonalInformation_UserImage= (PhysicalPath);
                obj.InsertPersonalInformation(pf);
            }
            else
            {                
                obj.UpdatePersonalInformation(pf);
            } 
            
            return RedirectToAction("ShowPersonalInformation"); 
        }

        #endregion Personal Information

        #region Contact Information
        public ActionResult ShowContactInformation()
        {
            int Id = Convert.ToInt32(Session["RegistrationId"]);
            DatabaseQueries obj = new DatabaseQueries();
            ViewBag.GetAllStates = obj.GetStateDropdownData();
            ViewBag.GetAllCities = obj.GetCityDropdownData();

            ContactInformation ci = new ContactInformation();
            ViewBag.GetContactInformationByRegistrationId = obj.GetContactInformationByRegistrationId(Id);

            return View("ShowContactInformation", ci);
        }
        public ActionResult EditContactInformation(FormCollection form)
        {
            int Id = Convert.ToInt32(form["ContactInformation_Id"].ToString());
            DatabaseQueries obj = new DatabaseQueries();
            ContactInformation ci = new ContactInformation();
            ViewBag.GetAllStates = obj.GetStateDropdownData();
            ViewBag.GetAllCities = obj.GetCityDropdownData();
            ci = obj.GetContactInformationByContactInformationId(Id);


            return View("EditContactInformation", ci);
        }
        [HttpPost]
        public ActionResult SaveContactInformation(ContactInformation ci)
        {
            ci.ContactInformation_RegistrationId = Convert.ToInt32(Session["RegistrationId"]);
            DatabaseQueries obj = new DatabaseQueries();
           
            
            if (ci.ContactInformation_Id == 0)
            {

                obj.InsertContactInformation(ci);
            }
            else
            {
                obj.UpdateContactInformation(ci);
                
            }

            return RedirectToAction("ShowContactInformation");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult LoadCityByState(string StateId)
        {
            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            List<City> c = new List<Models.City>();
            c = obj.GetCitybyStateId(Convert.ToInt32(StateId));

            var CityData = c.Select(m => new SelectListItem()
            {
                Text = m.City_Name,
                Value = m.City_Id.ToString()
            });
            return Json(CityData, JsonRequestBehavior.AllowGet);
        }

        #endregion Contact Information

        #region Qualification Information
        public ActionResult ShowQualificationInformation()
        {
            int Id = Convert.ToInt32(Session["RegistrationId"]);
            DatabaseQueries obj = new DatabaseQueries();
            Qualification qi = new Qualification();
            AdminDatabaseQueries obj2 = new AdminDatabaseQueries();
            ViewBag.GetEducationCategoryDropDownData=  obj2.GetEducationCategoryDropDownData();
            ViewBag.GetEducationTypeDropDownData = obj2.GetEducationTypeDropDownData();
            ViewBag.GetSubEducationTypeDropDownData = obj2.GetSubEducationTypeDropDownData();
            //ViewBag.EducationTypeDropDown = obj.GetEducationTypeDropdownData();
            //ViewBag.EducationSubTypeDropDown = obj.GetEducationSubTypeDropdownData();
            ViewBag.GetQualificationData = obj.GetQualificationData(Id);
            return View("ShowQualification", qi);
        }
        public ActionResult EditQualification(FormCollection form)
        {
            int Id = Convert.ToInt32(form["Qualification_Id"].ToString());
            DatabaseQueries obj = new DatabaseQueries();
            Qualification qi = new Qualification();
            AdminDatabaseQueries obj2 = new AdminDatabaseQueries();
            ViewBag.GetEducationCategoryDropDownData = obj2.GetEducationCategoryDropDownData();
            ViewBag.GetEducationTypeDropDownData = obj2.GetEducationTypeDropDownData();
            ViewBag.GetSubEducationTypeDropDownData = obj2.GetSubEducationTypeDropDownData();
            //ViewBag.EducationTypeDropDown = obj.GetEducationTypeDropdownData();
            //ViewBag.EducationSubTypeDropDown = obj.GetEducationSubTypeDropdownData();
            qi = obj.GetQualificationInformationByQualificationId(Id);       
            return View("EditQualification", qi);           
        }

        [HttpPost]
        public ActionResult SaveQualificationInformation(Qualification qi)
        {
            qi.Qualification_RegistrationId = Convert.ToInt32(Session["RegistrationId"]);
            DatabaseQueries obj = new DatabaseQueries();
            if (qi.Qualification_Id == 0)
            {

                obj.InsertQualificationInformation(qi);
            }
            else
            {
                obj.UpdateQualificationInformation(qi);

            }
            return RedirectToAction("ShowQualificationInformation");
        }

        #endregion Qualification Information


        #region Work Experience Information
        public ActionResult ShowWorkExperienceInformation()
        {
            int Id = Convert.ToInt32(Session["RegistrationId"]);
            DatabaseQueries obj = new DatabaseQueries();
            WorkExperience we = new WorkExperience();
           
            ViewBag.GetWorkExperienceData = obj.GetWorkExperienceData(Id);
            return View("ShowWorkExperience", we);
        }
        public ActionResult EditWorkExperience(FormCollection form)
        {
            int Id = Convert.ToInt32(form["WorkExperience_Id"].ToString());
            DatabaseQueries obj = new DatabaseQueries();
            WorkExperience we = new WorkExperience();
            we = obj.GetWorkExperienceInformationByWorkExperienceId(Id);
           
            return View("EditWorkExperience", we);
        }

        [HttpPost]
        public ActionResult SaveWorkExperienceInformation(WorkExperience we)
        {
            we.WorkExperience_RegisterationId = Convert.ToInt32(Session["RegistrationId"]);
            DatabaseQueries obj = new DatabaseQueries();
            if (we.WorkExperience_Id == 0)
            {
                obj.InsertWorkExperienceInformation(we);
            }
            else
            {
                obj.UpdateWorkExperienceInformation(we);
         
            }
            return RedirectToAction("ShowWorkExperienceInformation");
        }

        #endregion Qualification Information

      

        #region Jobs
        public ActionResult ShowCurrentJobOpenings()
        {
            //int reg = Convert.ToInt32(Session["RegistrationId"]);
            DatabaseQueries obj = new DatabaseQueries();
            JobOpening jo = new JobOpening();
            ViewBag.GetCurrentJobOpeningData = obj.GetCurrentJobOpeningData();
            //JobApplied ja = new JobApplied();

           // ViewBag.GetJobAppliedDataForCurrentOpenings = obj.GetJobAppliedDataForCurrentOpenings(reg);

            return View("ShowCurrentJobOpenings",jo); 
        }
    [HttpPost]
        public ActionResult ApplyJobs(JobApplied jo,FormCollection form)
        {
            DatabaseQueries obj = new DatabaseQueries();
            jo.JobApplied_JobOpenings_Id = Convert.ToDecimal(form["JobOpenings_Id"].ToString());
            jo.JobApplied_RegisterationId = Convert.ToDecimal(Session["RegistrationId"]);
            jo.JobApplied_ApplyDate = DateTime.Now;
            jo.JobApplied_Status = "waiting";
            ViewBag.GetjobappliedbyRegistrationId = obj.GetjobappliedbyRegistrationId(jo.JobApplied_RegisterationId);
        
            if (obj.ApplyJobDuplicationEntryCheck(jo))
            {
                RedirectToAction("ShowCurrentJobOpenings");
            }
            else
            {
                obj.ApplyJobSave(jo);
                 
            }
           
            
            return RedirectToAction("ShowCurrentJobOpenings"); 
        }

        public ActionResult AppliedJobs(JobApplied jo)
    {

        int JobApplied_RegisterationId = Convert.ToInt32(Session["RegistrationId"]);
        AdminDatabaseQueries obj2 = new AdminDatabaseQueries();
        ViewBag.GetUserJobAppliedData = obj2.GetUserJobAppliedData(JobApplied_RegisterationId);
           
                  
            
            
            return View("AppliedJobs",jo); 
        }
        public ActionResult MyAcceptedApplications(JobApplied jo)
        {
            int JobApplied_RegisterationId = Convert.ToInt32(Session["RegistrationId"]);
            DatabaseQueries obj = new DatabaseQueries();
            ViewBag.GetMyAcceptedJobApplications = obj.GetMyAcceptedJobApplications(JobApplied_RegisterationId);
            return View("MyAcceptedApplications");
        }
        #endregion Jobs

        #region Hired
        public ActionResult Hired()
        {
        int Id = Convert.ToInt32(Session["RegistrationId"]);
        DatabaseQueries obj = new DatabaseQueries();
        FinalSelection fs = new FinalSelection();


        ViewBag.GetHiredStatusByRegistrationId = obj.GetHiredStatusByRegistrationId(Id);
            return View("Hired",fs);
        }

        #endregion Hired



    }
}