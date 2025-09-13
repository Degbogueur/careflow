using CareFlow.Models;
using CareFlow.ViewModels.Consultations;
using System.Linq.Expressions;

namespace CareFlow.Mappers;

public static class ConsultationMappers
{
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
