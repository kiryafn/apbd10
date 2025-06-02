using HospitalAPI.Models;

namespace HospitalAPI.Repositories.Abstractions;

public interface IPrescriptionRepository
{
    Task AddAsync(Prescription prescription);
    Task<Prescription?> GetByIdAsync(int idPrescription);
    Task<List<Prescription>> GetByPatientIdAsync(int idPatient);
    Task SaveChangesAsync();
}