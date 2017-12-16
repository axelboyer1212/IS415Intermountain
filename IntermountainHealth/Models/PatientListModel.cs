using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using System.Data.Sql;

namespace IntermountainHealth.Models
{
    public class PatientListModel
    {
        public IEnumerable<PatientModel> Items;
        public void LoadItems()
        {
            //Items = context.PatientModel.ToList();
        }
    }
}