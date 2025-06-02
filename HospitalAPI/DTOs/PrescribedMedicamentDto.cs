namespace HospitalAPI.DTOs;

public class PrescribedMedicamentDto
{
    public int IdMedicament { get; set; }

    public int Dose { get; set; }

    public string Description { get; set; } = null!;
}