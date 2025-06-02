using HospitalAPI.Models;

namespace HospitalAPI.Repositories.Abstractions;

public interface IMedicamentRepository
{
    Task<Medicament?> GetByIdAsync(int idMedicament);
    Task<bool> ExistsAsync(int idMedicament);
}