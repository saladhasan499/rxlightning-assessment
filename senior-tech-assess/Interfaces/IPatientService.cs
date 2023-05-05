using System;
using senior_tech_assess.Models;

namespace senior_tech_assess.Interfaces
{
	public interface IPatientService
	{
        Task<List<PatientModel>> GetPatientsAsync();
        Task<PatientModel> GetPatientByIdAsync(string id);
    }
}

