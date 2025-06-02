using HospitalAPI.Models;

namespace HospitalAPI.Services.Abstractions;

public interface IMedicamentService
{
    Task<Medicament?> GetByIdAsync(int idMedicament);
    Task<bool> ExistsAsync(int idMedicament);
}