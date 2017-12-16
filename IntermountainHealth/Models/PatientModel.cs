using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntermountainHealth.Models
{
    public class PatientModel
    {
        public const string statusCURRENT = "C";
        public const string statusCHECKEDOUT = "O";

        public int Id { get; set; }
        public string Status { get; set; }
        public string PatientName { get; set; }
        public int DifficultyLevel { get; set; }
        public bool IsAbnormal { get; set; }
        public int Shift { get; set; }
        public double EstimatedHours { get; set; }
        public double ActualHours { get; set; }
    }
}