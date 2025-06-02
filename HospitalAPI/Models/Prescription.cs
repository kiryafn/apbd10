using System.ComponentModel.DataAnnotations;

namespace HospitalAPI.Models;

public class Prescription
{
    // EF Core по конвенции распознаёт PrescriptionId как PK
    public int PrescriptionId { get; set; }

    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }

    // если вы используете в базe MySQL/SQL свойства PatientId/DoctorId:
    public int PatientId { get; set; }
    public Patient Patient { get; set; } = null!;

    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; } = null!;

    [Timestamp]
    public byte[] RowVersion { get; set; } = null!;

    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = new List<PrescriptionMedicament>();
}