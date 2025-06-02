using HospitalAPI.DTOs;
using HospitalAPI.Repositories.Abstractions;
using HospitalAPI.Services.Abstractions;

namespace HospitalAPI.Services;

public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepo;
        private readonly IPrescriptionRepository _prescriptionRepo;

        public PatientService(IPatientRepository patientRepo, IPrescriptionRepository prescriptionRepo)
        {
            _patientRepo = patientRepo;
            _prescriptionRepo = prescriptionRepo;
        }

        public async Task<PatientDetailsDto?> GetPatientDetailsAsync(int idPatient)
        {
            var patient = await _patientRepo.GetByIdAsync(idPatient);
            if (patient == null) return null;

            var prescriptions = await _prescriptionRepo.GetByPatientIdAsync(idPatient);

            var result = new PatientDetailsDto
            {
                IdPatient = patient.PatientId,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                BirthDate = patient.BirthDate,
                Address = patient.Address,
                Email = patient.Email,
                Prescriptions = prescriptions.Select(p => new PrescriptionDetailsDto
                {
                    IdPrescription = p.PrescriptionId,
                    Date = p.Date,
                    DueDate = p.DueDate,
                    Doctor = new DoctorDto
                    {
                        IdDoctor = p.Doctor.DoctorId,
                        FirstName = p.Doctor.FirstName,
                        LastName = p.Doctor.LastName,
                        Email = p.Doctor.Email,
                        Phone = p.Doctor.Phone
                    },
                    Medicaments = p.PrescriptionMedicaments
                        .Select(pm => new MedicamentDetailsDto
                        {
                            IdMedicament = pm.MedicamentId,
                            Name = pm.Medicament.Name,
                            Dose = pm.Dose,
                            Description = pm.Details
                        })
                        .ToList()
                }).ToList()
            };

            return result;
        }
    }