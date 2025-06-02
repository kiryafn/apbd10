using HospitalAPI.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }

  
    [HttpGet("{idPatient:int}")]
    public async Task<IActionResult> GetPatientDetails(int idPatient)
    {
        var result = await _patientService.GetPatientDetailsAsync(idPatient);
        if (result == null)
            return NotFound(new { error = $"Patient with ID {idPatient} not found." });
        return Ok(result);
    }
}