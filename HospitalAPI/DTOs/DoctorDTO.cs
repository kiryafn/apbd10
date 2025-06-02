namespace HospitalAPI.DTOs;

public class DoctorDto
{
    public int? IdDoctor { get; set; } // optional: if provided, find existing

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;
}