using HospitalAPI.DTOs;
using HospitalAPI.Models;

namespace HospitalAPI.Services.Abstractions;

public interface IDoctorService
{
    Task<Doctor?> FindOrCreateAsync(DoctorDto dto);
}