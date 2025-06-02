using HospitalAPI.DTOs;
using HospitalAPI.Models;
using HospitalAPI.Repositories.Abstractions;
using HospitalAPI.Services.Abstractions;

namespace HospitalAPI.Services;

public class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _doctorRepo;

    public DoctorService(IDoctorRepository doctorRepo)
    {
        _doctorRepo = doctorRepo;
    }

    public async Task<Doctor?> FindOrCreateAsync(DoctorDto dto)
    {
        Doctor? doctor = null;

        if (dto.IdDoctor.HasValue)
        {
            doctor = await _doctorRepo.GetByIdAsync(dto.IdDoctor.Value);
            if (doctor != null) return doctor;
        }

        // Try to find by email
        doctor = await _doctorRepo.GetByEmailAsync(dto.Email);
        if (doctor != null) return doctor;

        // Create new
        var newDoc = new Doctor
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Phone = dto.Phone
        };
        await _doctorRepo.AddAsync(newDoc);
        await _doctorRepo.SaveChangesAsync();
        return newDoc;
    }
}