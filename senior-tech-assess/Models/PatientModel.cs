using System;
namespace senior_tech_assess.Models
{
    public class PatientModel
    {
        public string patientId { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string gender { get; set; }

        public string dateOfBirth { get; set; }

        public string addressLine1 { get; set; }

        public string addressLine2 { get; set; }

        public string city { get; set; }

        public string state { get; set; }

        public string postalCode { get; set; }

        //public PatientModel(string id, string fname, string lname, string gender, string dob, string add1, string add2, string city, string state, int postalCode)
        //{
        //    PatientId = id;
        //    FirstName = fname;
        //    LastName = lname;
        //    Gender = gender;
        //    DateOfBirth = dob;
        //    AddressLine1 = add1;
        //    AddressLine2 = add2;
        //    City = city;
        //    State = state;
        //    PostalCode = postalCode;
        //}
    }
}

