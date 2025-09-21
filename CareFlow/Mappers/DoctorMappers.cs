using CareFlow.Models;
using CareFlow.ViewModels.Doctors;
using CareFlow.ViewModels.Patients;
using System.Linq.Expressions;

namespace CareFlow.Mappers;

public static class DoctorMappers
{
    public static Doctor ToModel(this AddDoctorViewModel viewModel)
    {
        return new Doctor
        {
            FirstName = viewModel.FirstName,
            LastName = viewModel.LastName,
            Email = viewModel.Email,
            PhoneNumber = viewModel.PhoneNumber,
            SpecialtyId = viewModel.SpecialtyId
        };
    }

    public static Expression<Func<Doctor, DoctorViewModel>> ToExpressionViewModel()
    {
        return doctor => new DoctorViewModel
        {
            Id = doctor.Id,
            FirstName = doctor.FirstName,
            LastName = doctor.LastName,
            Email = doctor.Email,
            PhoneNumber = doctor.PhoneNumber,
            SpecialtyId = doctor.Specialty!.Id,
            SpecialtyName = doctor.Specialty!.Name,
            PatientsCount = doctor.Consultations.Select(c => c.MedicalRecord).Distinct().Count(),
            UpcomingAppointments = doctor.Appointments.Count(a => a.Status == AppointmentStatus.Scheduled),
            HasUserAccount = doctor.HasUserAccount,
            IsActive = doctor.User != null && doctor.User.IsActive
        };
    }

    public static Expression<Func<Doctor, DoctorDetailsViewModel>> ToDetailsViewModel()
    {
        return doctor => new DoctorDetailsViewModel
        {
            Id = doctor.Id,
            FullName = doctor.FullName,
            Email = doctor.Email,
            PhoneNumber = doctor.PhoneNumber,
            SpecialtyName = doctor.Specialty!.Name,
            Patients = doctor.Appointments
                             .Where(a => a.Patient != null)
                             .Select(a => new PatientViewModel
                             {
                                 Id = a.Patient!.Id,
                                 FirstName = a.Patient.FirstName,
                                 LastName = a.Patient.LastName,
                                 Gender = a.Patient.Gender,
                                 Email = a.Patient.Email,
                                 PhoneNumber = a.Patient.PhoneNumber,
                                 Address = a.Patient.Address,
                                 Age = a.Patient.Age,
                                 RegistrationDate = a.Patient.RegistrationDate
                             })
                            .Distinct()
                            .ToList()
        };
    }
}
