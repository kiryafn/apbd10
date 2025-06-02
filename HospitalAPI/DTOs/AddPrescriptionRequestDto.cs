namespace HospitalAPI.DTOs;

public class AddPrescriptionRequestDto
{
    public PatientDto Patient { get; set; } = null!;

    public DoctorDto Doctor { get; set; } = null!;

    public PrescriptionRequestDto Prescription { get; set; } = null!;

    public List<PrescribedMedicamentDto> Medicaments { get; set; } = new();
}