using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using IntermountainHealth.DAL;
using IntermountainHealth.Models;
using RestSharp;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IntermountainHealth.Models
{
    public class PatientFormModel
    {
        private DataContext db = new DataContext();

        private Newtonsoft.Json.JsonSerializer serializer;

        public void NewtonsoftJsonSerializer(Newtonsoft.Json.JsonSerializer serializer)
        {
            this.serializer = serializer;
        }

        private int _id { get; set; }
        private int _difficulty { get; set; }
        private bool _isAbnormal { get; set; }
        private int _shift { get; set; }
        private string _patientIdentifier { get; set; }
        private bool? _isEdit;
        private double _actualHours { get; set; }

        public int Id { get; set; }
        public bool idFound { get; set; }

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

        [Display(Name = "Actual Hours")]
        public double? ActualHours { get; set; }

        public bool IsEdit;

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
            var patient = db.patients.Find(id);
            if (patient != null)
            {
                idFound = true;
                return patient;
            }
            else
            {
                idFound = false;
            }
            return new PatientModel();
        }
        public void Load(int id)
        {
            try
            {
                var patient = Find(id);
                if (patient != null)
                {
                    PatientIdentifier = patient.Patient_Identifier;
                    Difficulty = patient.Difficulty_Level.GetValueOrDefault();
                    Shift = patient.Nurse_Shift.GetValueOrDefault();
                    IsAbnormal = patient.Is_Abnormal.GetValueOrDefault();
                    EstimatedHours = patient.Estimated_Hours.GetValueOrDefault();
                    ActualHours = patient.Actual_Hours.Value;
                }
                else
                {

                }
            }
            catch
            { }

            //context.item.where id = id

            //if item is null return failure

            //set everything to item.property
        }

        public void Save()
        {
            int AbnormalInt;

            if (IsAbnormal)
            {
                AbnormalInt = 1;
            }
            else
            {
                AbnormalInt = 0;
            }
            var client = new RestClient("https://ussouthcentral.services.azureml.net/workspaces/697cda31aa1843d1a595dd556e5d2ab8/services/5c0ea97a23b246e596566c0393eafeb6/execute?api-version=2.0&details=true");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "d6a6d2e6-d524-df34-036b-deb6fa4cf800");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer ax/Lrhqvw+xtb0mHku6YDvysjvKlTacRNoqKwJk5RQld/2mlZ+TRfwB9pqRg144f8grIjftgCvWEQlU9PZ76pQ==");
            request.AddHeader("Content", "applic");
            request.AddParameter("application/json", "{\r\n  \"Inputs\": {\r\n    \"input1\": {\r\n      \"ColumnNames\": [\r\n        \"Difficulty_Level\",\r\n        \"IS_ABNORMAL\",\r\n        \"NURSE_SHIFT\"\r\n      ],\r\n      \"Values\": [\r\n        [\r\n          \"" + Difficulty + "\",\r\n          \"" + AbnormalInt + "\",\r\n          \"" + CalculateTime() + "\"\r\n        ]\r\n      ]\r\n    }\r\n  },\r\n  \"GlobalParameters\": {}\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            JObject stuff = JObject.Parse(response.Content);

            var estimatedHoursString = stuff["Results"]["output1"]["value"]["Values"][0][0].Value<string>();

            double convertedEstimatedHours;
            convertedEstimatedHours = double.Parse(estimatedHoursString);
            convertedEstimatedHours = Math.Round(convertedEstimatedHours, 1);

            try
            {
                var item = Find(Id);
                if (item == null || Id == 0)
                {
                    item = new PatientModel
                    {

                    };

                    item.Is_Abnormal = IsAbnormal;
                    item.Difficulty_Level = Difficulty;
                    item.Nurse_Shift = CalculateTime();
                    item.Patient_Identifier = PatientIdentifier;
                    item.Estimated_Hours = convertedEstimatedHours;

                    db.patients.Add(item);
                    db.SaveChanges();
                    Id = item.Patient_Id;
                }
                else
                {
                    item.Is_Abnormal = IsAbnormal;
                    item.Difficulty_Level = Difficulty;
                    item.Nurse_Shift = Shift;
                    item.Patient_Identifier = PatientIdentifier;
                    item.Estimated_Hours = convertedEstimatedHours;
                    item.Actual_Hours = ActualHours;

                    db.SaveChanges();
                }

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

        public T Deserialize<T>(RestSharp.IRestResponse response)
        {
            var content = response.Content;

            using (var stringReader = new StringReader(content))
            {
                using (var jsonTextReader = new JsonTextReader(stringReader))
                {
                    return serializer.Deserialize<T>(jsonTextReader);
                }
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