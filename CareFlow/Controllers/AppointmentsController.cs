using CareFlow.Models.Results;
using CareFlow.Services.Interfaces;
using CareFlow.ViewModels.Appointments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareFlow.Controllers;

[Authorize]
public class AppointmentsController(
    IAppointmentService appointmentService) : Controller
{
    public async Task<IActionResult> Index(PaginationParameters? parameters = null, CancellationToken cancellationToken = default)
    {
        var appointments = await appointmentService.GetAllAsync(parameters, cancellationToken);
        return View(appointments);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateAppointmentViewModel viewModel, CancellationToken cancellationToken = default)
    {
        await appointmentService.AddAsync(viewModel, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel(int id, CancellationToken cancellationToken = default)
    {
        await appointmentService.CancelAsync(id, cancellationToken);
        return RedirectToAction(nameof(Index));
    }
}
