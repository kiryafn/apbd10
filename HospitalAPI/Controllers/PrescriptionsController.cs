using HospitalAPI.DTOs;
using HospitalAPI.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionsController : ControllerBase
{
    private readonly IPrescriptionService _presService;

    public PrescriptionsController(IPrescriptionService presService)
    {
        _presService = presService;
    }
    
    
    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] AddPrescriptionRequestDto request)
    {
        if (request == null)
            return BadRequest("Request cannot be null.");

        try
        {
            await _presService.AddPrescriptionAsync(request);
            return Created(string.Empty, new { message = "Prescription created successfully." });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "An unexpected error occurred." });
        }
    }
}