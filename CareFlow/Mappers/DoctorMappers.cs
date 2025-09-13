using CareFlow.Models;
using CareFlow.ViewModels.Doctors;

namespace CareFlow.Mappers;

public static class DoctorMappers
{
    public static Doctor ToModel(this AddDoctorViewModel viewModel)
    {
        return new Doctor
        {
            FirstName = viewModel.FirstName,
            LastName = viewModel.LastName,
            SpecialtyId = viewModel.SpecialtyId
        };
    }
}
