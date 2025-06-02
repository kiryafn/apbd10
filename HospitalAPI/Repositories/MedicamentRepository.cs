using HospitalAPI.Data;
using HospitalAPI.Models;
using HospitalAPI.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Repositories;

public class MedicamentRepository : IMedicamentRepository
{
    private readonly ApplicationDbContext _context;

    public MedicamentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Medicament?> GetByIdAsync(int idMedicament)
    {
        return await _context.Medicaments.FindAsync(idMedicament);
    }

    public async Task<bool> ExistsAsync(int idMedicament)
    {
        return await _context.Medicaments.AnyAsync(m => m.MedicamentId == idMedicament);
    }
}