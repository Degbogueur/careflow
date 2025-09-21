using CareFlow.Models;
using CareFlow.ViewModels.Consultations;
using System.Linq.Expressions;

namespace CareFlow.Mappers;

public static class ConsultationMappers
{
    public static Consultation ToModel(this CreateConsultationViewModel viewModel)
    {
        return new Consultation
        {
            Date = viewModel.Date,
            DoctorId = viewModel.DoctorId,
            Weight = viewModel.Weight,
            Height = viewModel.Height,
            Temperature = viewModel.Temperature,
            Symptoms = viewModel.Symptoms,
            Diagnosis = viewModel.Diagnosis,
            Treatment = viewModel.Treatment,
            Notes = viewModel.Notes
        };
    }

    public static Expression<Func<Consultation, ConsultationViewModel>> ToViewModelExpression()
    {
        return consultation => new ConsultationViewModel
        {
            Id = consultation.Id,
            Date = consultation.Date,
            PatientName = consultation.MedicalRecord!.Patient!.FullName,
            DoctorName = consultation.Doctor!.FullName,
            Diagnosis = consultation.Diagnosis
        };
    }
}
