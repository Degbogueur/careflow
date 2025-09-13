using CareFlow.Models.Results;
using CareFlow.Services.Interfaces;
using CareFlow.ViewModels.Appointments;
using Microsoft.AspNetCore.Mvc;

namespace CareFlow.Controllers;

public class AppointmentsController(
    IAppointmentService appointmentService,
    IDoctorService doctorService,
    IPatientService patientService) : Controller
{
    public async Task<IActionResult> Index(PaginationParameters? parameters = null, CancellationToken cancellationToken = default)
    {
        var appointments = await appointmentService.GetAllAsync(parameters, cancellationToken);
        var doctors = await doctorService.GetSelectListItemsAsync(count: 10, cancellationToken);
        var patients = await patientService.GetSelectListItemsAsync(count: 10, cancellationToken);
        ViewBag.Doctors = doctors;
        ViewBag.Patients = patients;
        return View(appointments);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateAppointmentViewModel viewModel, CancellationToken cancellationToken = default)
    {
        await appointmentService.AddAsync(viewModel, cancellationToken);
        return RedirectToAction(nameof(Index));
    }
}
