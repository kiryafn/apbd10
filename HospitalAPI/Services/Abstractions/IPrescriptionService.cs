using HospitalAPI.DTOs;

namespace HospitalAPI.Services.Abstractions;

public interface IPrescriptionService
{
    Task<bool> AddPrescriptionAsync(AddPrescriptionRequestDto request);
}