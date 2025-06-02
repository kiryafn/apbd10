using HospitalAPI.DTOs;

namespace HospitalAPI.Services.Abstractions;

public interface IPatientService
{
    Task<PatientDetailsDto?> GetPatientDetailsAsync(int idPatient);
}