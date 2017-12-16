using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace IntermountainHealth.Models
{
    public class PatientFormModel
    {
        private int _id { get; set; }
        private int _difficulty { get; set; }
        private bool _isAbnormal { get; set; }
        private int _shift { get; set; }
        private string _patientIdentifier { get; set; }
        private bool? _isEdit;
        private double _actualHours { get; set; }

        public int Id { get; set; }

        [Display(Name ="Difficulty Level")]
        [Range(1, 3)]
        public int Difficulty { get; set; }

        [Display(Name = "Does this patient require abnormal tests?")]
        public bool IsAbnormal { get; set; }

        public int Shift { get; set; }

        [Display(Name = "Estimated Hours")]
        public double EstimatedHours { get; set; }

        [Display(Name = "Patient Identifier")]
        public string PatientIdentifier { get; set; }
        public double ActualHours { get; set; }

        public bool IsEdit
        {
            get
            {
                if (_isEdit == null) _isEdit = CheckEdit();
                return _isEdit.Value;
            }
        }

        public int CalculateTime()
        {
            var currentTime = DateTime.Now.Hour;
            if (currentTime > 18 || currentTime <= 6)
            {
                return 1;
            }
            else
            {
                return 0;
            }
            //night shift 1 18-6
            //day shift 0 6-18
        }

        public bool CheckEdit()
        {
            return true;
        }

        public PatientModel Find(int id)
        {
            //return _context.PatientModel.Find(id);
            return new PatientModel();
        }
        public void Load(int id)
        {
            //context.item.where id = id

            //if item is null return failure

            //set everything to item.property
        }

        public void Save()
        {
            try
            {
                var item = Find(Id);
                if (item == null)
                {
                    item = new PatientModel
                    {
                        //all values
                    };

                    //context.PatientModel.Add(item);
                }

                //set item values

                //context.SaveChanges();
            }
            catch (Exception ex)
            {
                // 
            }
        }

        public void Delete(int id)
        {
            try
            {
                var item = Find(id);
                if (item == null)
                {

                }

                try
                {
                    //context.Remove(item);
                    //context.SaveChanges();
                }
                catch (Exception ex)
                {

                }
            }
            catch
            {

            }
        }

        public IEnumerable <SelectListItem> ShiftList()
        {
            var list = new List<SelectListItem>();

            list.Add(new SelectListItem { Text = "Day Shift", Value = "0" });
            list.Add(new SelectListItem { Text = "Night Shift", Value = "1" });
            return list;
        }
    }
}