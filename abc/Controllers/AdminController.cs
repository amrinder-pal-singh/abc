using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using abc.Models;
using abc.DataAccess;
using Newtonsoft.Json;


namespace abc.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            return View();
        }
        //LogIn
        public ActionResult AdminLogIn()
        {
            Session["Admin"] = null;
            return View("Login", new AdminLogin());
        }

        [HttpPost]
        public ActionResult AfterAdminLogIn(AdminLogin adminLogIn)
        {
            DatabaseQueries obj = new DatabaseQueries();
            if (obj.CheckValidAdmin(adminLogIn))
            {
                adminLogIn = obj.GetAdminByUserName(adminLogIn.Admin_UserName);
                Session["Admin"] = adminLogIn;
                return RedirectToAction("HomePage");
            }
            return View("Login", new AdminLogin());
        }
        //HomePage
        public ActionResult HomePage()
        {

            return View();
        }


        #region Setup Job

        #region Designations
        public ActionResult ShowDesignationInformation()
        {
            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            Designation d = new Designation();
            ViewBag.GetDesignationData = obj.GetDesignationData();
            return View("Designation", d);
        }
        public ActionResult EditDesignationInformation(FormCollection form)
        {

            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            Designation d = new Designation();
            d = obj.GetDesignationInformationByDesignationId(Convert.ToInt32(form["Designation_Id"].ToString()));
            return View("EditDesignationInformation", d);
        }


        [HttpPost]
        public ActionResult SaveDesignationInformation(Designation d)
        {
            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            if (d.Designation_Id == 0)
            {
                obj.InsertDesignationInformation(d);
            }
            else
            {
                obj.UpdateDesignationInformation(d);
            }
            return RedirectToAction("ShowDesignationInformation");
        }
        #endregion Designations

        #region Job Openings

        public ActionResult ShowJobOpeningsInformation()
        {


            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            JobOpening jo = new JobOpening();
            ViewBag.GetJobOpeningDesignationDropdownData = obj.GetJobOpeningDesignationDropdownData();
            ViewBag.GetJobOpeningData = obj.GetJobOpeningData();
            return View("ShowJobOpenings", jo);
        }
        public ActionResult EditJobOpeningInformation(FormCollection form)
        {


            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            JobOpening d = new JobOpening();
            ViewBag.GetJobOpeningDesignationDropdownData = obj.GetJobOpeningDesignationDropdownData();
            d = obj.GetJobOpeningInformationByJobOpeningId(Convert.ToInt32(form["JobOpenings_Id"].ToString()));
            return View("EditJobOpeningInformation", d);
        }

        [HttpPost]
        public ActionResult SaveJobOpeningInformation(JobOpening jo)
        {
            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            if (jo.JobOpenings_Id == 0)
            {
                obj.InsertJobOpeningInformation(jo);
            }
            else
            {
                obj.UpdateJobOpeningInformation(jo);
            }
            return RedirectToAction("ShowJobOpeningsInformation");
        }
        [HttpPost]
        public ActionResult ChangeJobOpeningsStatus(FormCollection form)
        {
            int JobOpenings_Id = Convert.ToInt32(form["JobOpenings_Id"].ToString());
            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            obj.ChangeJobOpeningStatus(JobOpenings_Id);
            return RedirectToAction("ShowJobOpeningsInformation");
        }

        #endregion Job Openings


        #region Manage Applicants
        public ActionResult ViewJobApplicants()
        {
            AdminDatabaseQueries obj = new AdminDatabaseQueries();

            ViewBag.GetjobAppliedData = obj.GetjobAppliedData();
            return View("ViewJobApplicants");
        }
        public ActionResult ViewJobApplicantsCompleteInfo(FormCollection form)
        {
            int JobApplied_RegisterationId = Convert.ToInt32(form["JobApplied_RegisterationId"].ToString());

            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            ViewModels.ViewApplicantsViewmodel vm = new ViewModels.ViewApplicantsViewmodel();
            vm = obj.CompleteUserInformation(JobApplied_RegisterationId);
            return View("ViewJobApplicantsCompleteInfo", vm);
        }

        [HttpPost]
        public ActionResult AcceptApplicant(FormCollection form)
        {
            int JobApplied_Id = Convert.ToInt32(form["JobApplied_Id"].ToString());
            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            JobApplied jo = new JobApplied();

            obj.AcceptCandidate(JobApplied_Id);


            return RedirectToAction("ViewJobApplicants", jo);
        }



        public ActionResult RejectApplicant(FormCollection form)
        {
            int JobApplied_Id = Convert.ToInt32(form["JobApplied_Id"].ToString());
            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            obj.RejectCandidate(JobApplied_Id);

            return RedirectToAction("ViewJobApplicants");
        }
        public ActionResult ViewAcceptedApplicants()
        {
            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            ViewBag.GetAcceptedApplicant = obj.GetAcceptedApplicant();
            return View("ViewAcceptedApplicants");
        }

        public ActionResult ViewRejectedApplicants()
        {
            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            ViewBag.GetRejectedApplicant = obj.GetRejectedApplicant();
            return View("ViewRejectedApplicants");

        }
        #endregion Manage Applicants
        #region Schedule & Hirings
        public ActionResult ShowTodayZinterviewee()
        {
            AdminDatabaseQueries obj = new AdminDatabaseQueries();

            FinalSelection fs = new FinalSelection();
            ViewBag.GetTodayZInterviewee = obj.GetTodayZInterviewee();

            return View("ShowTodayZinterviewee");
        }

        public ActionResult ShowUpcomingInterviewee()
        {
            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            ViewBag.GetUpcomingInterviewee = obj.GetUpcomingInterviewee();


            return View("ShowUpcomingInterviewee");
        }




        #endregion Schedule & Hirings
        #region Interview Round
        public ActionResult ShowInterviewRounds()
        {
            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            InterViewRound ivr = new InterViewRound();
            ViewBag.GetInterviewRoundData = obj.GetInterviewRoundData();
            return View("ShowInterviewRounds", ivr);
        }
        public ActionResult EditInterviewRoundInformation(FormCollection form)
        {

            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            InterViewRound ivr = new InterViewRound();
            ivr = obj.GetInterviewRoundInformationInformationbyInterviewRoundId(Convert.ToInt32(form["InterviewRounds_Id"].ToString()));

            return View("EditInterviewRoundInformation", ivr);
        }


        [HttpPost]
        public ActionResult SaveInterviewRoundInformation(InterViewRound ivr)
        {
            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            if (ivr.InterviewRounds_Id == 0)
            {
                obj.InsertInterviewRound(ivr);
            }
            else
            {
                obj.UpdateInterviewRound(ivr);
            }
            return RedirectToAction("ShowInterviewRounds");
        }
        public ActionResult InterviewRoundChangeStatus(FormCollection form)
        {
            int InterviewRounds_Id = Convert.ToInt32(form["InterviewRounds_Id"].ToString());
            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            obj.ChangeInterviewRoundStatus(InterviewRounds_Id);
            return RedirectToAction("ShowInterviewRounds");
        }


        #endregion Interview Round
        #region FinalDisqualify
        public ActionResult FinalDisqualify(FormCollection form)
        {
            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            FinalDisqualification fs = new FinalDisqualification();
            ViewBag.InterviewRounDropDownData = obj.InterviewRounDropDownData();
            fs.FinalDisqualification_JobAppliedId = Convert.ToInt32(form["JobApplied_Id"].ToString());
            return View("FinalDisqualify", fs);

        }
        [HttpPost]
        public ActionResult SaveFinalDisqualification(FinalDisqualification fs)
        {
            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            if (fs.FinalDisqualification_Id == 0)
            {
                obj.FinalDisqualificationofCandidate(fs);
            }

            return RedirectToAction("ShowTodayZinterviewee");
        }
        public ActionResult ShowAllDisqualifiedCandidates()
        {
            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            ViewBag.GetFinallyDisqualifiedCandidates = obj.GetFinallyDisqualifiedCandidates();
            return View("ShowAllDisqualifiedCandidates");
        }


        #endregion FinalDisqualify
        #region FinalSelection


        public ActionResult ShowAllSelectedCandidates()
        {
            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            ViewBag.GetFinallySelectedCandidates = obj.GetFinallySelectedCandidates();
            return View("ShowAllSelectedCandidates");
        }
        public ActionResult FinalSelection(FormCollection form)
        {
            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            FinalSelection fs = new FinalSelection();
            ViewBag.InterviewRounDropDownData = obj.InterviewRounDropDownData();
            fs.FinalSelection_JobAppliedId = Convert.ToInt32(form["JobApplied_Id"].ToString());
            return View("FinalSelection", fs);

        }
        [HttpPost]
        public ActionResult SaveFinalSelection(FinalSelection fs)
        {
            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            if (fs.FinalSelection_Id == 0)
            {
                obj.FinalSelectionofCandidate(fs);
            }

            return RedirectToAction("ShowTodayZinterviewee");
        }

        #endregion FinalSelection

        #endregion Setup Job


     

        #region Education Setup


        #region Education Category

        #region Category
        public ActionResult ShowEducationCategory()
        {
            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            EducationCategory ec = new EducationCategory();
            ViewBag.GetEducationCategoryData = obj.GetEducationCategoryData();

            return View("EducationCategory", ec);
        }
        public ActionResult ChangeEducationCategoryStatus(FormCollection form)
        {
            int Education_CategoryId = Convert.ToInt32(form["Education_CategoryId"].ToString());
            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            obj.ChangeEducationCategoryStatus(Education_CategoryId);

            return RedirectToAction("ShowEducationCategory");
        }
        [HttpPost]
        public ActionResult SaveEducationCategory(EducationCategory ec)
        {
            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            if (ec.Education_CategoryId == 0)
            {
                obj.InsertEducationCategory(ec);
            }
            else
            {
                obj.UpdateEducationCategory(ec);

            }

            return RedirectToAction("ShowEducationCategory");
        }
        public ActionResult EditEducationCategory(FormCollection form)
        {
            int Id = Convert.ToInt32(form["Education_CategoryId"].ToString());
            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            EducationCategory ec = new EducationCategory();
            ec = obj.GetEducationCategoryByEducationCategoryId(Id);

            return View("EditEducationCategory", ec);
        }

        #endregion Category

        #region SubCategory
        public ActionResult ShowEducationType()
        {
            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            EducationType et = new EducationType();
            ViewBag.GetEducationTypeData = obj.GetEducationTypeData();
            ViewBag.GetEducationCategoryDropDownData = obj.GetEducationCategoryDropDownData();

            return View("ShowEducationType", et);

        }
        public ActionResult ChangeEducationTypeStatus(FormCollection form)
        {

            int Education_TypeId = Convert.ToInt32(form["EducationType_Id"].ToString());
            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            obj.ChangeEducationTypeStatus(Education_TypeId);

            return RedirectToAction("ShowEducationType");
        }
        [HttpPost]
        public ActionResult SaveEducationType(EducationType et)
        {
            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            if (et.EducationType_Id == 0)
            {
                obj.InsertEducationType(et);

            }
            else
            {
                obj.UpdateEducationType(et);


            }

            return RedirectToAction("ShowEducationType");
        }
        public ActionResult EditEducationType(FormCollection form)
        {
            int Education_TypeId = Convert.ToInt32(form["EducationType_Id"].ToString());
            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            EducationType et = new EducationType();
            ViewBag.GetEducationCategoryDropDownData = obj.GetEducationCategoryDropDownData();
            et = obj.GetEducationTypeByEducationTypeId(Education_TypeId);

            return View("EditEducationType", et);
        }
        #endregion SubCategory

        #region Education Subtype Urf Education Stream
        public ActionResult ShowEducationSubType()
        {
            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            EducationSubType est = new EducationSubType();

            ViewBag.GetEducationSubTypeData = obj.GetEducationSubTypeData();
            ViewBag.GetEducationCategoryDropDownData = obj.GetEducationCategoryDropDownData();
            ViewBag.GetEducationTypeDropDownData = obj.GetEducationTypeDropDownData();

            return View("ShowEducationSubType", est);

        }
        public ActionResult ChangeEducationSubTypeStatus(FormCollection form)
        {

            int EducationSubType_Id = Convert.ToInt32(form["EducationSubType_Id"].ToString());
            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            obj.ChangeEducationSubTypeStatus(EducationSubType_Id);

            return RedirectToAction("ShowEducationSubType");
        }
        [HttpPost]
        public ActionResult SaveEducationSubType(EducationSubType est)
        {
            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            if (est.EducationSubType_Id == 0)
            {
                obj.InsertEducationSubType(est);

            }
            else
            {
                obj.UpdateEducationSubType(est);


            }

            return RedirectToAction("ShowEducationSubType");
        }
        public ActionResult EditEducationSubType(FormCollection form)
        {
            int Estid = Convert.ToInt32(form["EducationSubType_Id"].ToString());
            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            EducationSubType est = new EducationSubType();
            ViewBag.GetEducationCategoryDropDownData = obj.GetEducationCategoryDropDownData();
            ViewBag.GetEducationTypeDropDownData = obj.GetEducationTypeDropDownData();
            est = obj.GetEducationSubTypeByEducationSubTypeId(Estid);

            return View("EditEducationSubType", est);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult LoadEducationTypeByEducationCategory(string EducationCategoryId)
        {
            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            List<EducationType> et = new List<Models.EducationType>();
            et = obj.GetEducationTypebyEducationCategoryId(Convert.ToInt32(EducationCategoryId));

            var EducationTypeData = et.Select(m => new SelectListItem()
            {
                Text = m.EducationType_Name,
                Value = m.EducationType_Id.ToString()
            });
            return Json(EducationTypeData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult LoadSubTypeByEducationType(string SubEducationCategoryId)
        {
            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            List<EducationSubType> est = new List<EducationSubType>();
            est = obj.GetEducationSubTypebyEducationTypeId(Convert.ToInt32(SubEducationCategoryId));

            var EducationSubTypeData = est.Select(m => new SelectListItem()
            {
                Text = m.EducationSubType_Name,
                Value = m.EducationSubType_Id.ToString()
            });
            return Json(EducationSubTypeData, JsonRequestBehavior.AllowGet);
        }


        #endregion Education Subtype Urf Education Stream




        #endregion Education Category






        public ActionResult EducationSubType()
        {
            return View("EducationSubType");
        }


        #endregion Education Setup


        #region Location Setting
        #region State
        public ActionResult ShowState()
        {
            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            State st = new State();
            ViewBag.GetStatesData = obj.GetStatesData();

            return View("ShowState", st);
        }
        public ActionResult EditState()
        {

            return RedirectToAction("ShowState");
        }
        #endregion State
        #region City

        #endregion City
        #endregion Location Setting

        //Edit Admin Profile
        public ActionResult EditAdminProfile()
        {
            AdminLogin adminLogIn = new AdminLogin();
            adminLogIn = Session["Admin"] as AdminLogin;
            return View(adminLogIn);
        }

        [HttpPost]
        public string AdminEditUserName(string UserName)
        {
            string success = "";
            AdminLogin adminLogIn = new AdminLogin();
            adminLogIn = Session["Admin"] as AdminLogin;
            int AdminId = Convert.ToInt32(adminLogIn.Admin_Id);
            DatabaseQueries obj = new DatabaseQueries();
            if (obj.EditAdminUserName(UserName, AdminId))
            {
                success = UserName;
            }
            else
            {
                success = "Fail";
            }
            return JsonConvert.SerializeObject(success);
        }

        [HttpPost]
        public string AdminEditPassword(string Password)
        {
            string success = "";
            AdminLogin adminLogIn = new AdminLogin();
            adminLogIn = Session["Admin"] as AdminLogin;
            int AdminId = Convert.ToInt32(adminLogIn.Admin_Id);
            DatabaseQueries obj = new DatabaseQueries();
            if (obj.EditAdminPassword(Password, AdminId))
            {
                success = "Pass";
            }
            else
            {
                success = "Fail";
            }
            return JsonConvert.SerializeObject(success);
        }






        public ActionResult States()
        {
            DatabaseQueries obj = new DatabaseQueries();
            ViewBag.GetAllStates = obj.GetAllStates();
            return View();
        }

        public ActionResult AddEditStates(FormCollection form)
        {

            DatabaseQueries obj = new DatabaseQueries();
            ViewBag.GetStatesDropdownData = obj.GetStatesDropdownData();
            return View("StatesAddEdit", new State());
        }

        [HttpPost]
        public ActionResult SaveState(State state)
        {
            DatabaseQueries obj = new DatabaseQueries();
            if (state.State_Id == 0)
            {
                obj.InsertState(state);
            }
            else
            {
                obj.UpdateState(state);
            }
            return RedirectToAction("ShowState");
        }

        [HttpPost]
        public ActionResult EditState(FormCollection form)
        {
            int StateId = Convert.ToInt32(form["State_Id"].ToString());
            DatabaseQueries obj = new DatabaseQueries();
            State state = new State();
            state = obj.GetStateById(StateId);

            return View("StatesAddEdit", state);
        }

        [HttpPost]
        public ActionResult StateChangeStatus(FormCollection form)
        {
            int StateId = Convert.ToInt32(form["State_Id"].ToString());
            DatabaseQueries obj = new DatabaseQueries();
            obj.ChangeStateStatus(StateId);
            return RedirectToAction("ShowState");
        }

        //City
        public ActionResult Cities(City ct)
        {
            AdminDatabaseQueries obj = new AdminDatabaseQueries();
            ViewBag.Getcities = obj.Getcities();
            ViewBag.GetStateDropDown = obj.GetStateDropDown();

            return View("Cities", ct);
        }

        public ActionResult AddEditCities()
        {
            DatabaseQueries obj = new DatabaseQueries();
            //ViewBag.GetCountries = obj.GetCountriesDropdownData();
            ViewBag.GetStates = obj.GetStatesDropdownData();
            return View("CitiesAddEdit", new City());
        }

        [HttpPost]
        public ActionResult SaveCity(City c)
        {
            AdminDatabaseQueries obj = new AdminDatabaseQueries();

            if (c.City_Id == 0)
            {
                obj.InsertCity(c);
            }
            else
            {
                obj.UpdateCity(c);
            }
            return RedirectToAction("Cities");
        }

        [HttpPost]
        public ActionResult EditCity(FormCollection form)
        {
            int CityId = Convert.ToInt32(form["City_Id"].ToString());
            DatabaseQueries obj = new DatabaseQueries();
            City city = new City();
            city = obj.GetCityById(CityId);
            //ViewBag.GetCountries = obj.GetCountriesDropdownData();
            ViewBag.GetStates = obj.GetStatesDropdownData();
            return View("CitiesAddEdit", city);
        }

        [HttpPost]
        public ActionResult CityChangeStatus(FormCollection form)
        {
            int CityId = Convert.ToInt32(form["City_Id"].ToString());
            DatabaseQueries obj = new DatabaseQueries();
            obj.ChangeCityStatus(CityId);
            return RedirectToAction("Cities");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult LoadStatesByCountry(string CountryId)
        {
            DatabaseQueries obj = new DatabaseQueries();
            List<State> states = new List<State>();
            //states = obj.GetStatesByCountryId(Convert.ToInt32(CountryId));            
            var stateData = states.Select(m => new SelectListItem()
            {
                Text = m.State_Name,
                Value = m.State_Id.ToString()
            });
            return Json(stateData, JsonRequestBehavior.AllowGet);
        }
    }
}