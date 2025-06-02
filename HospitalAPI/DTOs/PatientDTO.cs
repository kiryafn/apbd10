namespace HospitalAPI.DTOs;

public class PatientDto
{
    public int? IdPatient { get; set; } // optional: if provided, try to find existing

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public string Address { get; set; } = null!;

    public string Email { get; set; } = null!;
}