using CareFlow.Models;
using CareFlow.ViewModels.Specialties;

namespace CareFlow.Mappers;

public static class SpecialtyMappers
{
    public static Specialty ToModel(this AddSpecialtyViewModel viewModel)
    {
        return new Specialty
        {
            Name = viewModel.Name,
            Description = viewModel.Description,
        };
    }
}
