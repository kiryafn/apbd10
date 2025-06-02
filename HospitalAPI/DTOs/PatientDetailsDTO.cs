namespace HospitalAPI.DTOs;

public class PatientDetailsDto
{
    public int IdPatient { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public string Address { get; set; } = null!;

    public string Email { get; set; } = null!;

    public List<PrescriptionDetailsDto> Prescriptions { get; set; } = new();
}