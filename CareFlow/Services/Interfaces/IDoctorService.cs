using CareFlow.Models.Results;
using CareFlow.ViewModels.Doctors;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CareFlow.Services.Interfaces;

public interface IDoctorService
{
    Task AddAsync(AddDoctorViewModel viewModel, CancellationToken cancellationToken);
    Task<PagedResult<DoctorViewModel>> GetAllAsync(PaginationParameters? parameters, CancellationToken cancellationToken);
    Task<List<SelectListItem>> GetSelectListItemsAsync(int count, CancellationToken cancellationToken);
}
