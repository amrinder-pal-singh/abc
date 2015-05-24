using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using abc.Models;

namespace abc.DataAccess
{
    public class ContextProvider
    {
        protected RecruitmentZoneEntities  db = null;
        public ContextProvider()
        {
            db = new RecruitmentZoneEntities();
        }
    }
}