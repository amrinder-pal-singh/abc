using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using abc.Models;

namespace abc.ViewModels
{
    public class ViewApplicantsViewmodel
    {
        public decimal UserRegisteration_RegisterationId { get; set; }
        public string UserRegistration_UserName { get; set; }
        public bool UserRegistration_Status { get; set; }

        public virtual List<ContactInformation> ContactInformations { get; set; }
       
        
        public virtual List<PersonalInformation> PersonalInformations { get; set; }
        public virtual List<Qualification> Qualifications { get; set; }
        public virtual List<WorkExperience> WorkExperiences { get; set; }
        public virtual List<JobApplied> JobApplieds { get; set; }








        //#region befaltu for the time being




























        ////Personal Information
        //public decimal PersonalInformation_Id { get; set; }
        //public decimal UserRegistration_RegistrationId { get; set; }
        //public string PersonalInformation_FirstName { get; set; }
        //public string PersonalInformation_LastName { get; set; }
        //public string PersonalInformation_DateOfBirth { get; set; }
        //public bool PersonalInformation_Gender { get; set; }
        //public bool PersonalInformation_MaritialStatus { get; set; }
        //public string PersonalInformation_UserImage { get; set; }

        ////Contact Information
        //public decimal ContactInformation_Id { get; set; }
        //public decimal ContactInformation_RegistrationId { get; set; }
        //public decimal ContactInformation_StateId { get; set; }
        //public decimal ContactInformation_CityId { get; set; }
        //public string ContactInformation_Address { get; set; }
        //public string ContactInformation_MobileNo { get; set; }
        //public string ContactInformation_AlternateMobileNo { get; set; }
        //public string ContactInformation_EmailId { get; set; }
       
        ////Qualification Information
        //public decimal Qualification_Id { get; set; }
        //public decimal Qualification_RegistrationId { get; set; }
        //public decimal Qualification_EducationTypeId { get; set; }
        //public decimal Qualification_EducationSubTypeId { get; set; }
        //public string Qualification_BoardUniversity { get; set; }
        //public int Qualification_PassingYear { get; set; }
        //public int Qualification_Percentage { get; set; }
        //public decimal Qualification_EducationCategoryId { get; set; }
      
        ////Work Experience Information
        //public decimal WorkExperience_Id { get; set; }
        //public decimal WorkExperience_RegisterationId { get; set; }
        //public string WorkExperience_CompanyName { get; set; }
        //public string WorkExperience_Designation { get; set; }
        //public string WorkExperience_FromDate { get; set; }
        //public string WorkExperience_ToDate { get; set; }
        //public string WorkExperience_Address { get; set; }


        //#endregion befaltu for the time being

    }
    }
