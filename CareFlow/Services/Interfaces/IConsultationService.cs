using CareFlow.Models.Results;
using CareFlow.ViewModels.Consultations;

namespace CareFlow.Services.Interfaces;

public interface IConsultationService
{
    Task CreateAsync(CreateConsultationViewModel viewModel, CancellationToken cancellationToken);
    Task<PagedResult<ConsultationViewModel>> GetAllAsync(PaginationParameters? parameters, CancellationToken cancellationToken);
}
