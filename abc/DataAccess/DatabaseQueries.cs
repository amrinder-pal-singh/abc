using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using abc.Models;
using System.Data.Entity;
using System.Web.Mvc;

namespace abc.DataAccess
{
    public class DatabaseQueries:ContextProvider
    {


        public void SaveFeedback(FeedBack fb)
        {
            db.FeedBacks.Add(fb);
          
            db.SaveChanges();
        }





        #region UserRegistrationLogin
        public bool CheckValidUser(UserRegistration ur)
        {
            bool Check = false;
            UserRegistration var = db.UserRegistrations.FirstOrDefault(d => d.UserRegistration_UserName == ur.UserRegistration_UserName);
            if (var != null)
            {
                if (var.UserRegistration_Password== ur.UserRegistration_Password && var.UserRegistration_Status == true)
                {
                    Check = true;
                }
            }
            return Check;
        }
        public void Register(UserRegistration ur)
        {
            ur.UserRegistration_Status = true;
            db.UserRegistrations.Add(ur);
            db.SaveChanges();
        }
        public UserRegistration GetUserByUserName(string UserName)
        {
            return db.UserRegistrations.FirstOrDefault(d => d.UserRegistration_UserName == UserName);
            
        }


        #endregion UserRegistrationLogin


        #region Personal Information
        public void InsertPersonalInformation(PersonalInformation pf)
        {
            db.PersonalInformations.Add(pf);
            db.SaveChanges();
        }
        public void UpdatePersonalInformation(PersonalInformation pf)
        {
            db.Entry(pf).State = EntityState.Modified;
            db.SaveChanges();
        }
        public List<PersonalInformation> GetPersonalInformationByRegistrationId(int Id)
        {
            return db.PersonalInformations.Where(d => d.PersonalInformation_RegistrationId == Id).ToList();
        }
      
        public PersonalInformation GetPersonalInformationByPersonalInformationId(int Id)
        {
            return db.PersonalInformations.FirstOrDefault(d => d.PersonalInformation_Id == Id);
        }
      

        #endregion


        #region Contact Information
        public void InsertContactInformation(ContactInformation ci)
        {
            db.ContactInformations.Add(ci);
            db.SaveChanges();
        }
        public void UpdateContactInformation(ContactInformation ci)
        {
            db.Entry(ci).State = EntityState.Modified;
            db.SaveChanges();
        }

        public List<ContactInformation> GetContactInformationByRegistrationId(int Id)
        {
            return db.ContactInformations.Where(d => d.ContactInformation_RegistrationId == Id).ToList();
        }
        public ContactInformation GetContactInformationByContactInformationId(int Id)
        {
            return db.ContactInformations.FirstOrDefault(d => d.ContactInformation_Id == Id);
        }
       
        
        #endregion

        #region Qualification Information
        public void InsertQualificationInformation(Qualification qi)
        {
            db.Qualifications.Add(qi);
            db.SaveChanges();
        }
        public void UpdateQualificationInformation(Qualification qi)
        {
            db.Entry(qi).State = EntityState.Modified;
            db.SaveChanges();
        }

        public Qualification GetQualificationInformationByRegistrationId(int Id)
        {
            Qualification que = new Qualification();
            que=db.Qualifications.FirstOrDefault(d => d.Qualification_RegistrationId == Id);
            return que;
        }  
        public List<Qualification> GetQualificationData(int Id)
        {
            return db.Qualifications.Where(d=>d.Qualification_RegistrationId==Id).ToList();
        }

        public Qualification GetQualificationInformationByQualificationId(int Id)
        {
            return db.Qualifications.FirstOrDefault(d => d.Qualification_Id == Id);
        }

        public List<SelectListItem> GetEducationTypeDropdownData()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "--Select EducationType--", Value = "0" });
            foreach (var item in db.EducationTypes)
            {
                list.Add(new SelectListItem { Text = item.EducationType_Name, Value = item.EducationType_Id.ToString() });
            }
            return list;
        }

        public List<SelectListItem> GetEducationSubTypeDropdownData()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "--Select EducationSubType--", Value = "0" });
            foreach (var item in db.EducationSubTypes)
            {
                list.Add(new SelectListItem { Text = item.EducationSubType_Name, Value = item.EducationSubType_Id.ToString() });
            }
            return list;
        }
        #endregion

        #region Work Experience Information
        public void InsertWorkExperienceInformation(WorkExperience we)
        {
            db.WorkExperiences.Add(we);
            
            db.SaveChanges();
        }
        public void UpdateWorkExperienceInformation(WorkExperience we)
        {
            db.Entry(we).State = EntityState.Modified;
            db.SaveChanges();
        }

        public WorkExperience GetWorkExperienceInformationByRegistrationId(int Id)
        {
            
            WorkExperience We = new WorkExperience();
            We = db.WorkExperiences.FirstOrDefault(d=>d.WorkExperience_RegisterationId==Id);
            
            return We;
        }
        public List<WorkExperience> GetWorkExperienceData(int Id)
        {
            return db.WorkExperiences.Where(d=>d.WorkExperience_RegisterationId==Id).ToList();
        }

        public WorkExperience GetWorkExperienceInformationByWorkExperienceId(int Id)
        {
            return db.WorkExperiences.FirstOrDefault(d => d.WorkExperience_Id == Id);
        }

    
        #endregion Work Experience Information

        #region CurrentJobOpenings

        public List<JobOpening> GetCurrentJobOpeningData()
        {
            JobApplied ja = new JobApplied();

            return db.JobOpenings.Where(d => d.JobOpenings_Status == true).ToList();
            
        }

        //public List<JobApplied> GetJobAppliedDataForCurrentOpenings(int reg)
        //{

        //    return db.JobApplieds.Where(d => d.JobApplied_RegisterationId == reg && d.JobApplied_Id != null).ToList();
        //}
        //public bool GetJobAppliedDataForCurrentOpenings()
        //{
        //    JobApplied jo = new JobApplied();
        //    bool Check = false;
        //    JobApplied var = db.JobApplieds.FirstOrDefault(d => d.JobApplied_RegisterationId == jo.JobApplied_RegisterationId && d.JobApplied_Id!=null);
        //    if (var != null)
        //    {
        //        Check = true;
        //    }
        //    return Check;

        //}
        public void ApplyJobSave(JobApplied jo)
        {
 
            db.JobApplieds.Add(jo);
            db.SaveChanges();
        }
     
     
        public List<JobApplied> GetjobappliedbyRegistrationId(decimal Id)
        {
         return db.JobApplieds.Where(d=>d.JobApplied_RegisterationId==Id).ToList();
        }
        public bool jobappliedEligiblefortest(int Id)
        {
            bool success = false;
            try
            {
                JobApplied ja = new JobApplied();
                ja = db.JobApplieds.FirstOrDefault(d => d.JobApplied_RegisterationId == Id && d.JobApplied_Status=="true");
                
                success = true;
            }
            catch (Exception)
            {
                throw;

            }
            return success;
        }

      





        public bool ApplyJobDuplicationEntryCheck(JobApplied jo)
        {
            bool Check = false;
            JobApplied var=db.JobApplieds.FirstOrDefault(d=>d.JobApplied_JobOpenings_Id==jo.JobApplied_JobOpenings_Id && d.JobApplied_RegisterationId ==jo.JobApplied_RegisterationId);
            if (var!=null)
            {
                Check = true;
            }
            return Check;
 
        }
      

        public List<JobApplied> GetMyAcceptedJobApplications(int Id)
        {
            return db.JobApplieds.Where(d => d.JobApplied_Status == "Accepted" && d.JobApplied_RegisterationId==Id).ToList();

        }



















        #endregion CurrentJobOpenings











        #region AdminSetup

        public bool CheckValidAdmin(AdminLogin adminLogin)
        {
            bool Check = false;
            AdminLogin var = db.AdminLogins.FirstOrDefault(d => d.Admin_UserName == adminLogin.Admin_UserName);
            if (var != null)
            {
                if (var.Admin_Password == adminLogin.Admin_Password && var.Admin_Status == true)
                {
                    Check = true;
                }
            }
            return Check;
        }

        //Edit Admin Profile
        public AdminLogin GetAdminById(int Id)
        {
            return db.AdminLogins.FirstOrDefault(d => d.Admin_Id == Id);
        }

        public AdminLogin GetAdminByUserName(string UserName)
        {
            return db.AdminLogins.FirstOrDefault(d => d.Admin_UserName == UserName);
        }

        public bool EditAdminUserName(string UserName, int Id)
        {
            bool success = false;
            try
            {
                AdminLogin adminLogIn = new AdminLogin();
                adminLogIn = db.AdminLogins.FirstOrDefault(d => d.Admin_Id == Id);
                adminLogIn.Admin_UserName = UserName;
                db.Entry(adminLogIn).State = EntityState.Modified;
                db.SaveChanges();
                success = true;
            }
            catch (Exception)
            {
                throw;
            }
            return success;
        }


        public bool EditAdminPassword(string Password, int Id)
        {
            bool success = false;
            try
            {
                AdminLogin adminLogIn = new AdminLogin();
                adminLogIn = db.AdminLogins.FirstOrDefault(d => d.Admin_Id == Id);
                adminLogIn.Admin_Password = Password;
                db.Entry(adminLogIn).State = EntityState.Modified;
                db.SaveChanges();
                success = true;
            }
            catch (Exception)
            {
                throw;
            }
            return success;
        }

        #endregion AdminSetup
    

      
      
   
        #region LocationSetup
        public List<State> GetAllStates()
        {
            return db.States.ToList();
        }
        public List<SelectListItem> GetStateDropdownData()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "--Select State--", Value = "0" });
            foreach (var item in db.States)
            {
                list.Add(new SelectListItem { Text = item.State_Name, Value = item.State_Id.ToString() });
            }
            return list;
        }

        public List<SelectListItem> GetCityDropdownData()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "--Select City--", Value = "0" });
            foreach (var item in db.Cities)
            {
                list.Add(new SelectListItem { Text = item.City_Name, Value = item.City_Id.ToString() });
            }
            return list;
        }
        public void InsertState(State State)
        {
            State.State_Status = true;
            db.States.Add(State);
            db.SaveChanges();
        }

        public State GetStateById(int Id)
        {
            return db.States.FirstOrDefault(d => d.State_Id == Id);
        }

        public void UpdateState(State State)
        {
            db.Entry(State).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void ChangeStateStatus(int Id)
        {
            State State = new State();
            State = db.States.FirstOrDefault(d => d.State_Id == Id);
            if (State.State_Status)
            {
                State.State_Status = false;
            }
            else
            {
                State.State_Status = true;
            }
            db.SaveChanges();
        }

        //Cities
        public List<City> GetAllCities()
        {
            return db.Cities.ToList();
        }

        public void InsertCity(City city)
        {
            city.City_Status = true;
           
            db.Cities.Add(city);
            db.SaveChanges();
        }

        public City GetCityById(int Id)
        {
            return db.Cities.FirstOrDefault(d => d.City_Id == Id);
        }

        public void UpdateCity(City city)
        {
            db.Entry(city).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void ChangeCityStatus(int Id)
        {
            City city = new City();
            city = db.Cities.FirstOrDefault(d => d.City_Id == Id);
            if (city.City_Status)
            {
                city.City_Status = false;
            }
            else
            {
                city.City_Status = true;
            }
            db.SaveChanges();
        }

   
        public List<SelectListItem> GetCitiesByStateIdDropdownData(int Id)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "-Please Select-", Value = "0" });
            foreach (var item in db.Cities.Where(d => d.City_StateId == Id).ToList())
            {
                list.Add(
                            new SelectListItem
                            {
                                Text = item.City_Name,
                                Value = item.City_Id.ToString()
                            }
                    );
            }
            return list;
        }

       
        public List<SelectListItem> GetStatesDropdownData()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "-Please Select-", Value = "0" });
            foreach (var item in db.States)
            {
                list.Add(
                            new SelectListItem
                            {
                                Text = item.State_Name,
                                Value = item.State_Id.ToString()
                            }
                    );
            }
            return list;
        }
        #endregion LocationSetup

        #region hired
        public List<FinalSelection> GetHiredStatusByRegistrationId(int Id)
        {
            return db.FinalSelections.Where(d => d.JobApplied.JobApplied_RegisterationId == Id).ToList();
        }
        #endregion Hired


    }
}