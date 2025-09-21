using CareFlow.Models.Results;
using CareFlow.ViewModels.Appointments;

namespace CareFlow.Services.Interfaces;

public interface IAppointmentService
{
    Task AddAsync(CreateAppointmentViewModel viewModel, CancellationToken cancellationToken);
    Task<PagedResult<AppointmentViewModel>> GetAllAsync(PaginationParameters? parameters, CancellationToken cancellationToken);
    Task<bool> CancelAsync(int id, CancellationToken cancellationToken);
}
