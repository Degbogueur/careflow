using CareFlow.Models.Results;
using CareFlow.Services.Interfaces;
using CareFlow.ViewModels.Consultations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareFlow.Controllers;

[Authorize]
public class ConsultationsController(
    IConsultationService consultationService) : Controller
{
    public async Task<IActionResult> Index(PaginationParameters? parameters = null, CancellationToken cancellationToken = default)
    {
        var consultations = await consultationService.GetAllAsync(parameters, cancellationToken);
        return View(consultations);
    }

    public IActionResult Create(int? appointmentId = null, CancellationToken cancellationToken = default)
    {
        var model = appointmentId.HasValue
                  ? new CreateConsultationViewModel { AppointmentId = appointmentId }
                  : new CreateConsultationViewModel();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateConsultationViewModel model, CancellationToken cancellationToken = default)
    {
        return RedirectToAction(nameof(Index));
    }
}
