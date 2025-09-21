using CareFlow.Models;
using CareFlow.ViewModels.Patients;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace CareFlow.Mappers;

public static class PatientMappers
{
    public static Patient ToModel(this AddPatientViewModel viewModel)
    {
        return new Patient
        {
            FirstName = viewModel.FirstName,
            LastName = viewModel.LastName,
            Email = viewModel.Email,
            DateOfBirth = viewModel.DateOfBirth,
            Gender = viewModel.Gender,
            PhoneNumber = viewModel.PhoneNumber,
            Address = viewModel.Address,
            RegistrationDate = DateTime.Now
        };
    }

    public static Expression<Func<Patient, PatientViewModel>> ToViewModelExpression()
    {
        return patient => new PatientViewModel
        {
            Id = patient.Id,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Gender = patient.Gender,
            PhoneNumber = patient.PhoneNumber,
            Address = patient.Address,
            RegistrationDate = patient.RegistrationDate,
            Age = patient.Age,
            HasUserAccount = patient.HasUserAccount,
            Email = patient.Email
        };
    }

    public static Expression<Func<Patient, UpdatePatientViewModel>> ToUpdateViewModelExpression()
    {
        return patient => new UpdatePatientViewModel
        {
            Id = patient.Id,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Email = patient.Email,
            PhoneNumber = patient.PhoneNumber,
            DateOfBirth = patient.DateOfBirth,
            Gender = patient.Gender,
            Address = patient.Address
        };
    }

    public static SetPropertyCalls<Patient> UpdateModel(SetPropertyCalls<Patient> patient, UpdatePatientViewModel viewModel)
    {
        return patient.SetProperty(p => p.FirstName, viewModel.FirstName)
                      .SetProperty(p => p.LastName, viewModel.LastName)
                      .SetProperty(p => p.DateOfBirth, viewModel.DateOfBirth)
                      .SetProperty(p => p.Gender, viewModel.Gender)
                      .SetProperty(p => p.PhoneNumber, viewModel.PhoneNumber)
                      .SetProperty(p => p.Address.Street, viewModel.Address.Street)
                      .SetProperty(p => p.Address.City, viewModel.Address.City)
                      .SetProperty(p => p.Address.Province, viewModel.Address.Province)
                      .SetProperty(p => p.Address.PostalCode, viewModel.Address.PostalCode)
                      .SetProperty(p => p.Address.Country, viewModel.Address.Country);
    }

    public static Expression<Func<Patient, PatientDetailsViewModel>> ToDetailsViewModel()
    {
        return patient => new PatientDetailsViewModel
        {
            Id = patient.Id,
            FullName = patient.FullName,
            DateOfBirth = patient.DateOfBirth,
            Age = patient.Age,
            Gender = patient.Gender,
            PhoneNumber = patient.PhoneNumber,
            Address = patient.Address,
            RegistrationDate = patient.RegistrationDate,
            Email = patient.Email
        };
    }
}
