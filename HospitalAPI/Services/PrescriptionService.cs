using HospitalAPI.DTOs;
using HospitalAPI.Models;
using HospitalAPI.Repositories.Abstractions;
using HospitalAPI.Services.Abstractions;

namespace HospitalAPI.Services;

public class PrescriptionService : IPrescriptionService
    {
        private const int MaxMedicamentsPerPrescription = 10;

        private readonly IPrescriptionRepository _presRepo;
        private readonly IPatientRepository _patientRepo;
        private readonly IDoctorService _doctorService;
        private readonly IMedicamentService _medService;

        public PrescriptionService(
            IPrescriptionRepository presRepo,
            IPatientRepository patientRepo,
            IDoctorService doctorService,
            IMedicamentService medService)
        {
            _presRepo = presRepo;
            _patientRepo = patientRepo;
            _doctorService = doctorService;
            _medService = medService;
        }

        /// <summary>
        /// Returns true if added successfully; throws exceptions on validation failures.
        /// </summary>
        public async Task<bool> AddPrescriptionAsync(AddPrescriptionRequestDto request)
        {
            // 1. Validate Prescription dates
            if (request.Prescription.DueDate < request.Prescription.Date)
            {
                throw new ArgumentException("DueDate must be greater than or equal to Date.");
            }

            // 2. Validate Medicament count
            if (request.Medicaments == null || request.Medicaments.Count == 0)
            {
                throw new ArgumentException("At least one medicament must be supplied.");
            }
            if (request.Medicaments.Count > MaxMedicamentsPerPrescription)
            {
                throw new ArgumentException($"A prescription can include at most {MaxMedicamentsPerPrescription} medicaments.");
            }

            // 3. Check that all prespecified Medicament IDs exist
            foreach (var pm in request.Medicaments)
            {
                bool exists = await _medService.ExistsAsync(pm.IdMedicament);
                if (!exists)
                    throw new ArgumentException($"Medicament with ID {pm.IdMedicament} does not exist.");
            }

            // 4. Resolve or create Patient
            var patDto = request.Patient;
            Patient? patient = null;
            if (patDto.IdPatient.HasValue)
            {
                patient = await _patientRepo.GetByIdAsync(patDto.IdPatient.Value);
            }
            if (patient == null)
            {
                // Try by email
                patient = await _patientRepo.GetByEmailAsync(patDto.Email);
            }
            if (patient == null)
            {
                patient = new Patient
                {
                    FirstName = patDto.FirstName,
                    LastName = patDto.LastName,
                    BirthDate = patDto.BirthDate,
                    Address = patDto.Address,
                    Email = patDto.Email
                };
                await _patientRepo.AddAsync(patient);
                await _patientRepo.SaveChangesAsync();
            }

            // 5. Resolve or create Doctor
            var doc = await _doctorService.FindOrCreateAsync(request.Doctor);
            if (doc == null)
                throw new InvalidOperationException("Unable to create or find the specified doctor.");

            // 6. Create Prescription
            var prescription = new Prescription
            {
                Date = request.Prescription.Date,
                DueDate = request.Prescription.DueDate,
                PatientId = patient.PatientId,
                Patient = patient,
                DoctorId = doc.DoctorId,
                Doctor = doc
            };

            // 7. Link each medicament via PrescriptionMedicament
            prescription.PrescriptionMedicaments = request.Medicaments
                .Select(pmDto => new PrescriptionMedicament
                {
                    MedicamentId = pmDto.IdMedicament,
                    Medicament = null!, // EF will fix this via foreign key
                    Dose = pmDto.Dose,
                    Details = pmDto.Description
                })
                .ToList();

            // 8. Persist
            await _presRepo.AddAsync(prescription);
            await _presRepo.SaveChangesAsync();

            return true;
        }
    }