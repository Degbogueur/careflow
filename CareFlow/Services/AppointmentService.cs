using CareFlow.Data;
using CareFlow.Extensions;
using CareFlow.Mappers;
using CareFlow.Models.Results;
using CareFlow.Services.Interfaces;
using CareFlow.ViewModels.Appointments;
using Microsoft.EntityFrameworkCore;

namespace CareFlow.Services;

public class AppointmentService(ApplicationDbContext dbContext) : IAppointmentService
{
    public async Task AddAsync(CreateAppointmentViewModel viewModel, CancellationToken cancellationToken = default)
    {
        var appointment = viewModel.ToModel();
        // TODO: Validation

        await dbContext.Appointments.AddAsync(appointment, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<PagedResult<AppointmentViewModel>> GetAllAsync
        (PaginationParameters? parameters = null, CancellationToken cancellationToken = default)
    {
        parameters ??= new PaginationParameters();
        return await dbContext.Appointments
            .AsNoTracking()
            .Select(AppointmentMappers.ToViewModelExpression())
            .ToPagedResultAsync(parameters, cancellationToken);
    }
}
