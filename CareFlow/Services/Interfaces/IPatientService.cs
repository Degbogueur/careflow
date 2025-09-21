using CareFlow.Models.Results;
using CareFlow.ViewModels.Patients;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CareFlow.Services.Interfaces;

public interface IPatientService
{
    Task AddAsync(AddPatientViewModel viewModel, CancellationToken cancellationToken);
    Task<PagedResult<PatientViewModel>> GetAllAsync(PaginationParameters? parameters, CancellationToken cancellationToken);
    Task<PatientViewModel?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<SearchResult>> SearchByNameAsync(string query, CancellationToken cancellationToken);
    Task<List<SelectListItem>> GetSelectListItemsAsync(int count, CancellationToken cancellationToken);
    Task<PatientDetailsViewModel?> GetPatientDetailsAsync(int id, CancellationToken cancellationToken);
    Task<UpdatePatientViewModel?> GetUpdateViewModelByIdAsync(int id, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(UpdatePatientViewModel viewModel, CancellationToken cancellationToken);
}
