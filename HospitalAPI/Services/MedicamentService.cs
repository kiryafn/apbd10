using HospitalAPI.Models;
using HospitalAPI.Repositories.Abstractions;
using HospitalAPI.Services.Abstractions;

namespace HospitalAPI.Services;

public class MedicamentService : IMedicamentService
{
    private readonly IMedicamentRepository _medRepo;

    public MedicamentService(IMedicamentRepository medRepo)
    {
        _medRepo = medRepo;
    }

    public async Task<Medicament?> GetByIdAsync(int idMedicament)
    {
        return await _medRepo.GetByIdAsync(idMedicament);
    }

    public async Task<bool> ExistsAsync(int idMedicament)
    {
        return await _medRepo.ExistsAsync(idMedicament);
    }
}