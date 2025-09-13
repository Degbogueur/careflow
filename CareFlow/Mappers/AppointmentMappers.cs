using CareFlow.Models;
using CareFlow.ViewModels.Appointments;
using System.Linq.Expressions;

namespace CareFlow.Mappers;

public static class AppointmentMappers
{
    public static Appointment ToModel(this CreateAppointmentViewModel viewModel)
    {
        return new Appointment
        {
            Date = viewModel.Date!.Value,
            StartTime = viewModel.StartTime!.Value,
            EndTime = viewModel.EndTime!.Value,
            PatientId = viewModel.PatientId,
            DoctorId = viewModel.DoctorId,
            Status = AppointmentStatus.Scheduled
        };
    }

    public static Expression<Func<Appointment, AppointmentViewModel>> ToViewModelExpression()
    {
        return appointment => new AppointmentViewModel
        {
            Id = appointment.Id,
            PatientName = appointment.Patient.FullName ?? string.Empty,
            DoctorName = appointment.Doctor.FullName ?? string.Empty,
            Date = appointment.Date,
            StartTime = appointment.StartTime,
            EndTime = appointment.EndTime,
            Status = appointment.Status
        };
    }
}
