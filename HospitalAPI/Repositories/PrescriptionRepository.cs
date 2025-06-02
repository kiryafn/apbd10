using HospitalAPI.Data;
using HospitalAPI.Models;
using HospitalAPI.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Repositories;

public class PrescriptionRepository : IPrescriptionRepository
{
    private readonly ApplicationDbContext _context;

    public PrescriptionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Prescription prescription)
    {
        await _context.Prescriptions.AddAsync(prescription);
    }

    public async Task<Prescription?> GetByIdAsync(int idPrescription)
    {
        return await _context.Prescriptions
            .Include(p => p.PrescriptionMedicaments)
            .ThenInclude(pm => pm.Medicament)
            .Include(p => p.Doctor)
            .Include(p => p.Patient)
            .FirstOrDefaultAsync(p => p.PrescriptionId == idPrescription);
    }

    public async Task<List<Prescription>> GetByPatientIdAsync(int idPatient)
    {
        return await _context.Prescriptions
            .Where(p => p.PatientId == idPatient)
            .Include(p => p.PrescriptionMedicaments)
            .ThenInclude(pm => pm.Medicament)
            .Include(p => p.Doctor)
            .OrderBy(p => p.DueDate)
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}