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
            .Select(d => new DoctorViewModel
            {
                Id = d.Id,
                FullName = d.FullName,
                SpecialtyName = d.Specialty!.Name,
                UpcomingAppointments = d.Appointments.Count(a => a.Status == AppointmentStatus.Scheduled),
                PatientsCount = d.Consultations.Select(c => c.MedicalRecord).Distinct().Count()
            }).ToPagedResultAsync(parameters, cancellationToken);
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
}
