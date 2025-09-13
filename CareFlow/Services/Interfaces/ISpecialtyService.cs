using CareFlow.Models.Results;
using CareFlow.ViewModels.Specialties;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CareFlow.Services.Interfaces;

public interface ISpecialtyService
{
    Task AddAsync(AddSpecialtyViewModel viewModel, CancellationToken cancellationToken);
    Task<PagedResult<SpecialtyViewModel>> GetAllAsync(PaginationParameters? parameters, CancellationToken cancellationToken);
    Task<List<SelectListItem>> GetSelectListItemsAsync(int count, CancellationToken cancellationToken);
    Task<List<SearchResult>> SearchByNameAsync(string query, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(UpdateSpecialtyViewModel viewModel, CancellationToken cancellationToken);
}
