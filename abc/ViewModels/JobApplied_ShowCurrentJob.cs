using abc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace abc.ViewModels
{
    public class JobApplied_ShowCurrentJob
    {
        public JobOpening JobOpening { get; set; }
        public JobApplied JobApplied { get; set; }
    }
}