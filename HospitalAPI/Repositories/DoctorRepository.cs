using HospitalAPI.Data;
using HospitalAPI.Models;
using HospitalAPI.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Repositories;


public class DoctorRepository : IDoctorRepository
{
    private readonly ApplicationDbContext _context;

    public DoctorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Doctor?> GetByIdAsync(int idDoctor)
    {
        return await _context.Doctors.FindAsync(idDoctor);
    }

    public async Task<Doctor?> GetByEmailAsync(string email)
    {
        return await _context.Doctors
            .FirstOrDefaultAsync(d => d.Email.ToLower() == email.ToLower());
    }

    public async Task AddAsync(Doctor doctor)
    {
        await _context.Doctors.AddAsync(doctor);
    }

    public void Update(Doctor doctor)
    {
        _context.Doctors.Update(doctor);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}