using CareFlow.Data;
using CareFlow.Extensions;
using CareFlow.Mappers;
using CareFlow.Models;
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

        var isAvailable = await IsAvailable(
            viewModel.DoctorId, viewModel.Date, viewModel.StartTime!.Value, viewModel.EndTime!.Value, cancellationToken);

        if (!isAvailable)
        {
            throw new Exception("There is already another appointment booked during this period");
        }

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

    public async Task<bool> CancelAsync(int id, CancellationToken cancellationToken = default)
    {
        var isUpdated = await dbContext.Appointments
            .Where(a => a.Id == id)
            .ExecuteUpdateAsync(a => a
                .SetProperty(a => a.Status, AppointmentStatus.Cancelled),
            cancellationToken);

        return isUpdated > 0;
    }

    private async Task<bool> IsAvailable(
        int doctorId, DateTime date, TimeSpan startDate, TimeSpan endDate, CancellationToken cancellationToken)
    {
        return !await dbContext.Appointments
            .AnyAsync(a => a.DoctorId == doctorId &&
                           a.Date == date &&
                           a.StartTime < endDate &&
                           a.EndTime > startDate &&
                           a.Status == AppointmentStatus.Scheduled,
            cancellationToken);
    }
}
