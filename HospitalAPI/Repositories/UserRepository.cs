using HospitalAPI.Data;
using HospitalAPI.Models;
using HospitalAPI.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByUsernameAsync(string username, CancellationToken token)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username, token);
    }

    public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken, CancellationToken token)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken, token);
    }

    public async Task AddUserAsync(User user, CancellationToken token)
    {
        await _context.Users.AddAsync(user, token);
    }

    public async Task SaveChangesAsync(CancellationToken token)
    {
        await _context.SaveChangesAsync(token);
    }
}