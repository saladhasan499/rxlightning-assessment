using System;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using senior_tech_assess.Models;
using senior_tech_assess.Interfaces;

namespace senior_tech_assess.Controllers;

[ApiController]
public class PatientController : ControllerBase
{


    private readonly ILogger<PatientController> _logger;
    private readonly IPatientService _patientService;

    public PatientController(ILogger<PatientController> logger, IPatientService patientService)
    {
        _logger = logger;
        _patientService = patientService;
    }


    [HttpGet("/patients")]
    public async Task<List<PatientModel>> GetPatientsAsync()
    {
        return await _patientService.GetPatientsAsync();
    }


    [HttpGet("/patients/{id}")]
    public async Task<PatientModel> GetPatientsByIdAsync(string id)
    {

        return await _patientService.GetPatientByIdAsync(id);
    }

    [HttpPost("login")]
    public IActionResult Authenticate([FromBody] LoginModel model)
    {
        if(model.Email == "user@example.com" && model.Password == "password123")
        {
            return Ok(new { Token = "tokenString" });
        }
        else
        {
            return Unauthorized();
        }
    }
}

