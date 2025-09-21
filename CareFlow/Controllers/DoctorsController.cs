using CareFlow.Models.Results;
using CareFlow.Services.Interfaces;
using CareFlow.ViewModels.Doctors;
using Microsoft.AspNetCore.Mvc;

namespace CareFlow.Controllers;

public class DoctorsController(
    IDoctorService doctorService,
    ISpecialtyService specialtyService) : Controller
{
    public async Task<IActionResult> Index(PaginationParameters? parameters = null, CancellationToken cancellationToken = default)
    {
        var doctors = await doctorService.GetAllAsync(parameters, cancellationToken);
        var specialties = await specialtyService.GetSelectListItemsAsync(count: 4, cancellationToken);
        ViewBag.Specialties = specialties;
        return View(doctors);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AddDoctorViewModel viewModel, CancellationToken cancellationToken = default)
    {
        await doctorService.AddAsync(viewModel, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UpdateDoctorViewModel viewModel, CancellationToken cancellationToken = default)
    {
        await doctorService.UpdateAsync(viewModel, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken = default)
    {
        var doctor = await doctorService.GetDoctorDetailsAsync(id, cancellationToken);
        return View(doctor);
    }

    [HttpGet]
    public async Task<IActionResult> Search(string query, CancellationToken cancellationToken = default)
    {
        var results = await doctorService.SearchByNameAsync(query, cancellationToken);
        return Ok(results.Select(s => new { id = s.Id, text = s.Text }));
    }
}
