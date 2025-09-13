using CareFlow.Models.Results;
using CareFlow.Services.Interfaces;
using CareFlow.ViewModels.Consultations;
using Microsoft.AspNetCore.Mvc;

namespace CareFlow.Controllers;

public class ConsultationsController(
    IConsultationService consultationService,
    IDoctorService doctorService,
    IPatientService patientService) : Controller
{
    public async Task<IActionResult> Index(PaginationParameters? parameters = null, CancellationToken cancellationToken = default)
    {
        var consultations = await consultationService.GetAllAsync(parameters, cancellationToken);
        return View(consultations);
    }

    public async Task<IActionResult> Create(CancellationToken cancellationToken = default)
    {
        var doctors = await doctorService.GetSelectListItemsAsync(count: 10, cancellationToken);
        var patients = await patientService.GetSelectListItemsAsync(count: 10, cancellationToken);
        var model = new CreateConsultationViewModel { Patients = patients, Doctors = doctors };
        return View(model);
    }
}
