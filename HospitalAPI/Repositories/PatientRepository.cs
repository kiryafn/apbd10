using HospitalAPI.Data;
using HospitalAPI.Models;
using HospitalAPI.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly ApplicationDbContext _context;

    public PatientRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Patient?> GetByIdAsync(int idPatient)
    {
        return await _context.Patients
            .Include(p => p.Prescriptions) // optionally include
            .FirstOrDefaultAsync(p => p.PatientId == idPatient);
    }

    public async Task<Patient?> GetByEmailAsync(string email)
    {
        return await _context.Patients
            .FirstOrDefaultAsync(p => p.Email.ToLower() == email.ToLower());
    }

    public async Task AddAsync(Patient patient)
    {
        await _context.Patients.AddAsync(patient);
    }

    public void Update(Patient patient)
    {
        _context.Patients.Update(patient);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}