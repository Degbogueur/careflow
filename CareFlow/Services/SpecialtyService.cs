using CareFlow.Data;
using CareFlow.Extensions;
using CareFlow.Mappers;
using CareFlow.Models.Results;
using CareFlow.Services.Interfaces;
using CareFlow.ViewModels.Specialties;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CareFlow.Services;

public class SpecialtyService(ApplicationDbContext dbContext) : ISpecialtyService
{
    public async Task AddAsync(AddSpecialtyViewModel viewModel, CancellationToken cancellationToken = default)
    {
        var specialty = viewModel.ToModel();
        // TODO: Validation

        await dbContext.Specialties.AddAsync(specialty, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<PagedResult<SpecialtyViewModel>> GetAllAsync(
        PaginationParameters? parameters = null, CancellationToken cancellationToken = default)
    {
        parameters ??= new PaginationParameters();
        return await dbContext.Specialties
            .AsNoTracking()
            .Select(s => new SpecialtyViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                DoctorsCount = s.Doctors.Count
            }).ToPagedResultAsync(parameters, cancellationToken);
    }

    public async Task<List<SelectListItem>> GetSelectListItemsAsync(int count = 10, CancellationToken cancellationToken = default)
    {
        return await dbContext.Specialties
            .AsNoTracking()
            .OrderBy(s => s.Name)
            .Take(count)
            .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name })
            .ToListAsync(cancellationToken);
    }

    public async Task<List<SearchResult>> SearchByNameAsync(string query, CancellationToken cancellationToken = default)
    {
        return await dbContext.Specialties
            .Where(s => EF.Functions.Like(s.Name, $"%{query}%"))
            .AsNoTracking()
            .OrderBy(s => s.Name)
            .Take(10)
            .Select(s => new SearchResult { Id = s.Id, Text = s.Name })
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> UpdateAsync(UpdateSpecialtyViewModel viewModel, CancellationToken cancellationToken = default)
    {
        var isUpdated = await dbContext.Specialties
            .Where(s => s.Id == viewModel.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(s => s.Name, viewModel.Name)
                .SetProperty(s => s.Description, viewModel.Description),
            cancellationToken);

        return isUpdated > 0;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var isDeleted = await dbContext.Specialties
            .Where(s => s.Id == id)
            .ExecuteDeleteAsync(cancellationToken);

            return isDeleted > 0;
    }
}
