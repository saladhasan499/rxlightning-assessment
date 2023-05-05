using System;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using senior_tech_assess.Models;
using senior_tech_assess.Interfaces;
using senior_tech_assess.Models;

public class PatientService : IPatientService
{
    private readonly IConfiguration _configuration;

    public PatientService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<List<PatientModel>> GetPatientsAsync()
    {
        string baseUrl = _configuration.GetValue<string>("ApiSettings:BaseUrl");
        using (var client = new HttpClient())
        {
            var response = await client.GetAsync($"{baseUrl}/patients");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var patients = JsonSerializer.Deserialize<List<PatientModel>>(content);
            patients.ForEach(e =>
            {
                var encodedBytes = System.Text.Encoding.UTF8.GetBytes(e.patientId);
                e.patientId = System.Convert.ToBase64String(encodedBytes);
            });
            return patients;
        }
    }

    public async Task<PatientModel> GetPatientByIdAsync(string encodedId)
    {
        var decodedBytes = System.Convert.FromBase64String(encodedId);
        var decodedId = System.Text.Encoding.UTF8.GetString(decodedBytes);
        string baseUrl = _configuration.GetValue<string>("ApiSettings:BaseUrl");
        using (var client = new HttpClient())
        {
            var response = await client.GetAsync($"{baseUrl}/patient/{decodedId}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var patient = JsonSerializer.Deserialize<PatientModel>(content);
            var encodedBytes = System.Text.Encoding.UTF8.GetBytes(patient.patientId);
            patient.patientId = System.Convert.ToBase64String(encodedBytes);
            return patient;

        }
    }
}
