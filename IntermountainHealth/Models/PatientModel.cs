using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntermountainHealth.Models
{
    [Table("PATIENT")]
    public class PatientModel
    {
        //public const string statusCURRENT = "C";
        //public const string statusCHECKEDOUT = "O";

        [Key]
        [Column("PATIENT_ID")]
        public int Patient_Id { get; set; }

        [Column("PATIENT_IDENTIFIER")]
        public string Patient_Identifier { get; set; }

        [Column("DIFFICULTY_LEVEL")]
        public int? Difficulty_Level { get; set; }

        [Column("IS_ABNORMAL")]
        public bool? Is_Abnormal { get; set; }

        [Column("NURSE_SHIFT")]
        public int? Nurse_Shift { get; set; }

        [Column("ESTIMATED_HOURS")]
        public Double? Estimated_Hours { get; set; }

        [Column("ACTUAL_HOURS")]
        public Double? Actual_Hours { get; set; }
    }
}