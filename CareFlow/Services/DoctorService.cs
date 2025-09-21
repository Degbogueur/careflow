using CareFlow.BackgroundJobs.Interfaces;
using CareFlow.Data;
using CareFlow.Extensions;
using CareFlow.Mappers;
using CareFlow.Models;
using CareFlow.Models.Results;
using CareFlow.Services.Interfaces;
using CareFlow.ViewModels.Doctors;
using Hangfire;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CareFlow.Services;

public class DoctorService(ApplicationDbContext dbContext) : IDoctorService
{
    public async Task AddAsync(AddDoctorViewModel viewModel, CancellationToken cancellationToken)
    {
        var doctor = viewModel.ToModel();
        // TODO: Validation

        await dbContext.Doctors.AddAsync(doctor, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        BackgroundJob.Enqueue<IUserAccountBackgroundJobs>(b => b.CreateDoctorUserAccountAsync(doctor.Id));
    }

    public async Task<PagedResult<DoctorViewModel>> GetAllAsync(PaginationParameters? parameters, CancellationToken cancellationToken)
    {
        parameters ??= new PaginationParameters();
        return await dbContext.Doctors
            .AsNoTracking()
            .Select(DoctorMappers.ToExpressionViewModel())
            .ToPagedResultAsync(parameters, cancellationToken);
    }

    public async Task<DoctorDetailsViewModel?> GetDoctorDetailsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Doctors
            .Where(d => d.Id == id)
            .AsNoTracking()
            .Select(DoctorMappers.ToDetailsViewModel())
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<SearchResult>> SearchByNameAsync(string query, CancellationToken cancellationToken = default)
    {
        var result = await dbContext.Doctors
            .Where(d => EF.Functions.Like(d.FirstName + " " + d.LastName, $"%{query}%"))
            .AsNoTracking()
            .OrderBy(d => d.FirstName)
                .ThenBy(d => d.LastName)
            .Take(10)
            .Select(d => new SearchResult { Id = d.Id, Text = d.FullName })
            .ToListAsync(cancellationToken);
        return result;
    }

    public async Task<List<SelectListItem>> GetSelectListItemsAsync(int count = 10, CancellationToken cancellationToken = default)
    {
        return await dbContext.Doctors
            .AsNoTracking()
            .OrderBy(d => d.FirstName)
                .ThenBy(d => d.LastName)
            .Take(count)
            .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.FullName })
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> UpdateAsync(UpdateDoctorViewModel viewModel, CancellationToken cancellationToken = default)
    {
        var isUpdated = await dbContext.Doctors
            .Where(d => d.Id == viewModel.Id)
            .ExecuteUpdateAsync(d => d
                .SetProperty(d => d.FirstName, viewModel.FirstName)
                .SetProperty(d => d.LastName, viewModel.LastName)
                .SetProperty(d => d.SpecialtyId, viewModel.SpecialtyId),
            cancellationToken);

        return isUpdated > 0;
    }
}
