using HospitalAPI.Models;

namespace HospitalAPI.Repositories.Abstractions;

public interface IDoctorRepository
{
    Task<Doctor?> GetByIdAsync(int idDoctor);
    Task<Doctor?> GetByEmailAsync(string email);
    Task AddAsync(Doctor doctor);
    void Update(Doctor doctor);
    Task SaveChangesAsync();
}