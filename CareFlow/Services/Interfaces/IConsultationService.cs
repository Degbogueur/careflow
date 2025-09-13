using CareFlow.Models.Results;
using CareFlow.ViewModels.Consultations;

namespace CareFlow.Services.Interfaces;

public interface IConsultationService
{
    Task<PagedResult<ConsultationViewModel>> GetAllAsync(PaginationParameters? parameters, CancellationToken cancellationToken);
}
