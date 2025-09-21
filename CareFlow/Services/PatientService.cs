using CareFlow.BackgroundJobs.Interfaces;
using CareFlow.Data;
using CareFlow.Extensions;
using CareFlow.Mappers;
using CareFlow.Models.Results;
using CareFlow.Services.Interfaces;
using CareFlow.ViewModels.Patients;
using Hangfire;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CareFlow.Services;

public class PatientService(ApplicationDbContext dbContext) : IPatientService
{
    public async Task AddAsync(AddPatientViewModel viewModel, CancellationToken cancellationToken = default)
    {
        var patient = viewModel.ToModel();
        // TODO: Validation

        await dbContext.Patients.AddAsync(patient, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        if (viewModel.CreateUserAccount)
            BackgroundJob.Enqueue<IUserAccountBackgroundJobs>(b => b.CreatePatientUserAccountAsync(patient.Id));
    }

    public async Task<PagedResult<PatientViewModel>> GetAllAsync(
        PaginationParameters? parameters = null, CancellationToken cancellationToken = default)
    {
        parameters ??= new PaginationParameters();
        return await dbContext.Patients
            .AsNoTracking()
            .Select(PatientMappers.ToViewModelExpression())
            .ToPagedResultAsync(parameters, cancellationToken);
    }

    public async Task<List<SelectListItem>> GetSelectListItemsAsync(int count = 10, CancellationToken cancellationToken = default)
    {
        return await dbContext.Patients
            .AsNoTracking()
            .OrderBy(p => p.FirstName)
                .ThenBy(p => p.LastName)
            .Take(count)
            .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.FullName })
            .ToListAsync(cancellationToken);
    }

    public async Task<PatientViewModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Patients
            .Where(p => p.Id == id)
            .AsNoTracking()
            .Select(PatientMappers.ToViewModelExpression())
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<SearchResult>> SearchByNameAsync(string query, CancellationToken cancellationToken = default)
    {
        var result = await dbContext.Patients
            .Where(d => EF.Functions.Like(d.FirstName + " " + d.LastName, $"%{query}%"))
            .AsNoTracking()
            .OrderBy(d => d.FirstName)
                .ThenBy(d => d.LastName)
            .Take(10)
            .Select(d => new SearchResult { Id = d.Id, Text = d.FullName })
            .ToListAsync(cancellationToken);
        return result;
    }

    public async Task<PatientDetailsViewModel?> GetPatientDetailsAsync(int id, CancellationToken cancellationToken)
    {
        return await dbContext.Patients
            .Where(p => p.Id == id)
            .AsNoTracking()
            .Select(PatientMappers.ToDetailsViewModel())
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<UpdatePatientViewModel?> GetUpdateViewModelByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Patients
            .Where(p => p.Id == id)
            .AsNoTracking()
            .Select(PatientMappers.ToUpdateViewModelExpression())
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> UpdateAsync(UpdatePatientViewModel viewModel, CancellationToken cancellationToken = default)
    {
        var isUpdated = await dbContext.Patients
            .Where(p => p.Id == viewModel.Id)
            .ExecuteUpdateAsync(p => p
                .SetProperty(p => p.FirstName, viewModel.FirstName)
                .SetProperty(p => p.LastName, viewModel.LastName)
                .SetProperty(p => p.Email, viewModel.Email)
                .SetProperty(p => p.PhoneNumber, viewModel.PhoneNumber)
                .SetProperty(p => p.DateOfBirth, viewModel.DateOfBirth)
                .SetProperty(p => p.Gender, viewModel.Gender)
                .SetProperty(p => p.Address.Street, viewModel.Address.Street)
                .SetProperty(p => p.Address.City, viewModel.Address.City)
                .SetProperty(p => p.Address.Province, viewModel.Address.Province)
                .SetProperty(p => p.Address.Country, viewModel.Address.Country)
                .SetProperty(p => p.Address.PostalCode, viewModel.Address.PostalCode),
            cancellationToken);

        return isUpdated > 0;
    }
}
