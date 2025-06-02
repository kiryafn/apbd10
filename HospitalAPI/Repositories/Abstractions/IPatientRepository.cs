using HospitalAPI.Models;

namespace HospitalAPI.Repositories.Abstractions;

public interface IPatientRepository
{
    Task<Patient?> GetByIdAsync(int idPatient);
    Task<Patient?> GetByEmailAsync(string email);
    Task AddAsync(Patient patient);
    void Update(Patient patient);
    Task SaveChangesAsync();
}