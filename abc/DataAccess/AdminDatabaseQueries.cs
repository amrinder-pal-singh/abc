using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using abc.Models;
using System.Data.Entity;
using System.Web.Mvc;
using abc.ViewModels;
namespace abc.DataAccess
{
    public class AdminDatabaseQueries:ContextProvider
    {

        #region Designation Information
        public void InsertDesignationInformation(Designation d)
        {
            db.Designations.Add(d);

            db.SaveChanges();
        }
        public void UpdateDesignationInformation(Designation d)
        {
            db.Entry(d).State = EntityState.Modified;
            db.SaveChanges();
        }

        
        public List<Designation> GetDesignationData()
        {
            return db.Designations.ToList();
       
        }

        public Designation GetDesignationInformationByDesignationId(int Id)
        {
            return db.Designations.FirstOrDefault(d => d.Designation_Id == Id);
       
        }


        #endregion Designation Information
        #region Job Opening Information

        public void InsertJobOpeningInformation(JobOpening jo)
        {
            db.JobOpenings.Add(jo);
            

            db.SaveChanges();
        }
        public void UpdateJobOpeningInformation(JobOpening jo)
        {
            JobOpening jo2 = new JobOpening();
            jo2 = db.JobOpenings.FirstOrDefault(d => d.JobOpenings_Id == jo.JobOpenings_Id);
            jo2.JobOpenings_Title = jo.JobOpenings_Title;
            jo2.JobOpenings_DesignationId = jo.JobOpenings_DesignationId;
            jo2.JobOpenings_Vaccancies = jo.JobOpenings_Vaccancies;
            jo2.JobOpenings_Location = jo.JobOpenings_Location;
            jo2.JobOpenings_WorkExperience = jo.JobOpenings_WorkExperience;
            jo2.JobOpenings_PostDateTime = jo.JobOpenings_PostDateTime;
            jo2.JobOpenings_JobType = jo.JobOpenings_JobType;
            jo2.JobOpenings_InterviewDate = jo.JobOpenings_InterviewDate;
            jo2.JobOpening_InterviewVenue = jo.JobOpening_InterviewVenue;
            db.SaveChanges();
        }


        public List<JobOpening> GetJobOpeningData()
        {
            return db.JobOpenings.ToList();

        }

        public JobOpening GetJobOpeningInformationByJobOpeningId(int Id)
        {

            return db.JobOpenings.FirstOrDefault(d => d.JobOpenings_Id == Id);

        }
        public List<SelectListItem> GetJobOpeningDesignationDropdownData()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "--Select Designations--", Value = "0" });
            foreach (var item in db.Designations)
            {
                list.Add(new SelectListItem { Text = item.Designation_Name, Value = item.Designation_Id.ToString() });
            }
            return list;
        }
        public void ChangeJobOpeningStatus(int Id)
        {
            JobOpening jo = new JobOpening();
            jo = db.JobOpenings.FirstOrDefault(d => d.JobOpenings_Id == Id);
            if (jo.JobOpenings_Status)
            {
                jo.JobOpenings_Status = false;
            }
            else
            {
                jo.JobOpenings_Status = true;
            }
            db.SaveChanges();
        }


        public List<JobApplied> DisplayAppliedJob()
        {
            return db.JobApplieds.ToList();
        }
        
        #endregion Job Opening Information
     
        #region Applicant Selection
        #region HandlingJob Applicants
        public List<JobApplied> GetjobAppliedData()
        {
            return db.JobApplieds.Where(d=>d.JobApplied_Status=="waiting").ToList();

          //  return db.JobApplieds.Where(d => d.JobApplied_Id == 2).Include(d=>d.JobApplied_RegisterationId==1).ToList();
           // return db.JobApplieds.Where(d => d.JobApplied_RegisterationId == 0 && d.JobApplied_RegisterationId == 0).ToList();
        }
        public List<JobApplied> GetRejectedApplicant()
        {
            return db.JobApplieds.Where(d => d.JobApplied_Status =="Rejected").ToList();
        }
        public List<JobApplied> GetAcceptedApplicant()
        {
            return db.JobApplieds.Where(d => d.JobApplied_Status == "Accepted").ToList(); 
        }
        public List<JobApplied> GetTodayZInterviewee()
        {
            JobApplied ja = new JobApplied();

            List<FinalSelection> fs= new List<FinalSelection>();
            fs = db.FinalSelections.ToList();
            int[] a = new int[50];
            int i=0;
            foreach (var item in fs)
            {//wait
                a[i] = Convert.ToInt32(item.FinalSelection_JobAppliedId);
                i++;
            }
            List<FinalDisqualification> fd = new List<FinalDisqualification>();
            fd = db.FinalDisqualifications.ToList();
            int[] b = new int[50];
            int j = 0;
            foreach (var item in fd)
            {
                b[j] = Convert.ToInt32(item.FinalDisqualification_JobAppliedId);
                j++;
            }
            return db.JobApplieds.Where(d => d.JobApplied_Status == "Accepted" && d.JobOpening.JobOpenings_InterviewDate == DateTime.Today && !(a.Contains((int)d.JobApplied_Id)) && !(b.Contains((int)d.JobApplied_Id))).ToList();
        }
        public List<FinalSelection> GetFinallySelectedCandidates()
        {
            return db.FinalSelections.ToList();
        }
        public List<FinalDisqualification> GetFinallyDisqualifiedCandidates()
        {
            return db.FinalDisqualifications.ToList();
        }
        public List<JobApplied> GetUpcomingInterviewee()
        {
            return db.JobApplieds.Where(d => d.JobApplied_Status == "Accepted" && d.JobOpening.JobOpenings_InterviewDate > DateTime.Today).ToList();
        }
       public List<JobApplied> GetUserJobAppliedData(int Id)
        {
           

           return db.JobApplieds.Where(d =>d.JobApplied_RegisterationId == Id && d.JobApplied_Status!="Accepted").ToList();
        }

       public void InsertJobAppliedRemark( JobApplied jo)
       {
           db.JobApplieds.Add(jo);

           db.SaveChanges();
       }
       public void UpdateJobAppliedRemark(JobApplied jo)
       {
          
           db.Entry(jo).State = EntityState.Modified;
           db.SaveChanges();
       }
       public void AcceptCandidate(int Id)
       {
           JobApplied ja = new JobApplied();
         
           ja = db.JobApplieds.FirstOrDefault(d => d.JobApplied_Id == Id);
           if (ja.JobApplied_Status=="waiting")
           {
               ja.JobApplied_Status = "Accepted";
               db.Entry(ja).State = EntityState.Modified;
               
           }
          
           db.SaveChanges();
       }
       public JobApplied GetJobAppliedInfoByJobAppliedId(int Id)
       {
           return db.JobApplieds.FirstOrDefault(d => d.JobApplied_Id == Id);


       }
           public void RejectCandidate(int Id)
       {
           JobApplied ja = new JobApplied();

          ja = db.JobApplieds.FirstOrDefault(d => d.JobApplied_Id == Id);
        
           if (ja.JobApplied_Status == "waiting")
           {
               ja.JobApplied_Status = "Rejected";

           }

           db.SaveChanges();
       }



           #endregion HandlingJob Applicants

           #region Rounds
           public void InsertInterviewRound(InterViewRound ivr)
           {
               db.InterViewRounds.Add(ivr);

               db.SaveChanges();
           }
           public void UpdateInterviewRound(InterViewRound ivr)
           {

               db.Entry(ivr).State = EntityState.Modified;
               db.SaveChanges();
           }
           public List<InterViewRound> GetInterviewRoundData()
           {
               return db.InterViewRounds.ToList();

           }

           public InterViewRound GetInterviewRoundInformationInformationbyInterviewRoundId(int Id)
           {
               return db.InterViewRounds.FirstOrDefault(d => d.InterviewRounds_Id == Id);

           }
           public void ChangeInterviewRoundStatus(int Id)
           {
               InterViewRound ivr = new InterViewRound();
               ivr = db.InterViewRounds.FirstOrDefault(d => d.InterviewRounds_Id == Id);
               if (ivr.Round_Status)
               {
                   ivr.Round_Status=false;
               }
               else
               {
                   ivr.Round_Status =true;
               }
               db.SaveChanges();
           }

           #endregion Rounds

           #region Final Disqualify
           public FinalDisqualification GetFinalDisqualificationByFinalDisqualificationId(int Id)
           {
               return db.FinalDisqualifications.FirstOrDefault(d => d.FinalDisqualification_Id == Id);

           }
         public void FinalDisqualificationofCandidate(FinalDisqualification fd)
           {
               db.FinalDisqualifications.Add(fd);

               db.SaveChanges();
           }
         public List<SelectListItem> InterviewRounDropDownData()
         {
             List<SelectListItem> list = new List<SelectListItem>();
             list.Add(new SelectListItem { Text = "-Please Select-", Value = "0" });
             foreach (var item in db.InterViewRounds)
             {
                 list.Add(
                             new SelectListItem
                             {
                                 Text = item.Round_Name,
                                 Value = item.InterviewRounds_Id.ToString()
                             }
                     );
             }
             return list;
         }
           #endregion Final Disqualify
    
       
        #region Final Selection
         public FinalSelection GetFinalSelectionByFinalSelectionId(int Id)
         {
             return db.FinalSelections.FirstOrDefault(d => d.FinalSelection_Id == Id);

         }
        public void FinalSelectionofCandidate(FinalSelection fs)
         {
             db.FinalSelections.Add(fs);

             db.SaveChanges();
         }



           #endregion Final Selection

        #endregion Applicant Selection

        #region Education Setup

           #region Education Category
           public List<EducationCategory> GetEducationCategoryData()
        {
            return db.EducationCategories.ToList();
          
        }
       
      
        public void ChangeEducationCategoryStatus(int Id)
        {
            EducationCategory ec = new EducationCategory();
            
            ec = db.EducationCategories.FirstOrDefault(d => d.Education_CategoryId == Id);
            if (ec.Education_CategoryStatus)
            {
                ec.Education_CategoryStatus = false;
            }
            else
            {
                ec.Education_CategoryStatus = true;
             
            }
            db.SaveChanges();
        }
        public void InsertEducationCategory(EducationCategory ec)
        {
            db.EducationCategories.Add(ec);

            db.SaveChanges();
        }
        public void UpdateEducationCategory(EducationCategory ec)
        {
            db.Entry(ec).State = EntityState.Modified;
            db.SaveChanges();
        }
        public EducationCategory GetEducationCategoryByEducationCategoryId(int Id)
        {
            return db.EducationCategories.FirstOrDefault(d=>d.Education_CategoryId==Id);
           

        }
       #endregion Education Category

        #region Education Type
        public List<EducationType> GetEducationTypeData()
        {
            return db.EducationTypes.ToList();
            

        }
        public void ChangeEducationTypeStatus(int Id)
        {
            EducationType et = new EducationType();
           

            et = db.EducationTypes.FirstOrDefault(d => d.EducationType_Id == Id);
            if (et.EducationType_Status==true)
            {
                et.EducationType_Status = false;
               
            }
            else if(et.EducationType_Status==false)
            {
                et.EducationType_Status = true;
               

            }
            db.SaveChanges();
        }
        public void InsertEducationType(EducationType et)
        {
            db.EducationTypes.Add(et);
            db.SaveChanges();
        }
        public void UpdateEducationType(EducationType et)
        {
            db.Entry(et).State = EntityState.Modified;
            db.SaveChanges();
        }
        public EducationType GetEducationTypeByEducationTypeId(int Id)
        {
            return db.EducationTypes.FirstOrDefault(d=>d.EducationType_Id==Id);
        }
        public List<EducationType> GetEducationTypebyEducationCategoryId(int Id)
        {
            return db.EducationTypes.Where(d => d.EducationType_EducationCategoryId == Id).ToList();
        }
        public List<EducationSubType> GetEducationSubTypebyEducationTypeId(int Id)
        {
            return db.EducationSubTypes.Where(d => d.EducationSubType_EducationTypeId == Id).ToList();
            
        }
      
        #endregion Education Type

        #region Education Subtype Urf Education Stream
        public List<EducationSubType> GetEducationSubTypeData()
        {
            return db.EducationSubTypes.ToList();


        }
        public void ChangeEducationSubTypeStatus(int Id)
        {
            EducationSubType est = new EducationSubType();


            est = db.EducationSubTypes.FirstOrDefault(d => d.EducationSubType_Id == Id);
            if (est.EducationSubType_Status == true)
            {
                est.EducationSubType_Status = false;

            }
            else if (est.EducationSubType_Status == false)
            {
                est.EducationSubType_Status = true;


            }
            db.SaveChanges();
        }
        public void InsertEducationSubType(EducationSubType est)
        {
            db.EducationSubTypes.Add(est);
            db.SaveChanges();
        }
        public void UpdateEducationSubType(EducationSubType est)
        {
            db.Entry(est).State = EntityState.Modified;
            db.SaveChanges();
        }
        public EducationSubType GetEducationSubTypeByEducationSubTypeId(int Id)
        {
            return db.EducationSubTypes.FirstOrDefault(d => d.EducationSubType_Id == Id);
        }
       
        




        #endregion Education Subtype Urf Education Stream




        #endregion Education Setup
        #region CommonDropDowns


        #region Education
        public List<SelectListItem> GetEducationCategoryDropDownData()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "-Please Select-", Value = "0" });
            foreach (var item in db.EducationCategories)
            {
                list.Add(
                            new SelectListItem
                            {
                                Text = item.Education_CategoryName,
                                Value = item.Education_CategoryId.ToString()
                            }
                    );
            }
            return list;
        }

        public List<SelectListItem> GetEducationTypeDropDownData()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "-Please Select-", Value = "0" });
            foreach (var item in db.EducationTypes)
            {
                list.Add(
                            new SelectListItem
                            {
                                Text = item.EducationType_Name,
                                Value = item.EducationType_Id.ToString()
                            }
                    );
            }
            return list;
        }

        public List<SelectListItem> GetSubEducationTypeDropDownData()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "-Please Select-", Value = "0" });
            foreach (var item in db.EducationSubTypes)
            {
                list.Add(
                            new SelectListItem
                            {
                                Text = item.EducationSubType_Name,
                                Value = item.EducationSubType_Id.ToString()
                            }
                    );
            }
            return list;
        }

        #endregion Education

        public ViewApplicantsViewmodel CompleteUserInformation(int Id)
        {
            UserRegistration ur = new UserRegistration();
            ur=db.UserRegistrations.FirstOrDefault(d=>d.UserRegisteration_RegisterationId==Id);
            ViewApplicantsViewmodel vm = new ViewApplicantsViewmodel();
            vm.ContactInformations = ur.ContactInformations.ToList();
            vm.PersonalInformations = ur.PersonalInformations.ToList();
            vm.Qualifications = ur.Qualifications.ToList();
            vm.WorkExperiences = ur.WorkExperiences.ToList();
            vm.JobApplieds = ur.JobApplieds.ToList();
            vm.UserRegisteration_RegisterationId = ur.UserRegisteration_RegisterationId;
            vm.UserRegistration_UserName = ur.UserRegistration_UserName;
            vm.UserRegistration_Status = ur.UserRegistration_Status;
            return vm;
        }




        #region Location
        public List<State> GetStatesData()
        {
            return db.States.ToList(); 
        }
        public List<SelectListItem> GetStateDropDown()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "--Please Select-", Value = "0" });
            foreach (var item in db.States)
            {
                list.Add(
                new SelectListItem{
                    Text=item.State_Name,
                    Value=item.State_Id.ToString()
                }
                );
                
            }
            return list;
        }
        public List<City> Getcities()
        {
            return db.Cities.ToList(); 
        }
        public void InsertCity(City c)
        {
            c.City_Status = true;
            db.Cities.Add(c);

            db.SaveChanges();
        }
        public void UpdateCity(City c)
        {
            db.Entry(c).State = EntityState.Modified;
            db.SaveChanges();
        }

        public List<City> GetCitybyStateId(int Id)
        {
            return db.Cities.Where(d => d.City_StateId == Id).ToList();
        }
        #endregion Location

        #endregion CommonDropDowns

    }
}