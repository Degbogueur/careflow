using CareFlow.Models.Results;
using CareFlow.Services.Interfaces;
using CareFlow.ViewModels.Patients;
using Microsoft.AspNetCore.Mvc;

namespace CareFlow.Controllers;

public class PatientsController(IPatientService patientService) : Controller
{
    public async Task<IActionResult> Index(PaginationParameters? parameters = null, CancellationToken cancellationToken = default)
    {
        var patients = await patientService.GetAllAsync(parameters, cancellationToken);
        return View(patients);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(AddPatientViewModel viewModel, CancellationToken cancellationToken = default)
    {
        await patientService.AddAsync(viewModel, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken = default)
    {
        var patient = await patientService.GetUpdateViewModelByIdAsync(id, cancellationToken);
        return View(patient);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UpdatePatientViewModel viewModel, CancellationToken cancellationToken = default)
    {
        await patientService.UpdateAsync(viewModel, cancellationToken);
        return RedirectToAction(nameof(Index));
    }
}
