namespace HospitalAPI.Models;

public class Patient
{
    public int PatientId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public string Address { get; set; } = null!;

    public string Email { get; set; } = null!;

    public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}