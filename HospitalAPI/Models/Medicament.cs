namespace HospitalAPI.Models;

public class Medicament
{
    public int MedicamentId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Type { get; set; } = null!;

    // Many-to-many join with prescriptions
    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = new List<PrescriptionMedicament>();
}