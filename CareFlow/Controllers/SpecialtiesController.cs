using CareFlow.Models.Results;
using CareFlow.Services.Interfaces;
using CareFlow.ViewModels.Specialties;
using Microsoft.AspNetCore.Mvc;

namespace CareFlow.Controllers;

public class SpecialtiesController(ISpecialtyService specialtyService) : Controller
{
    public async Task<IActionResult> Index(PaginationParameters? parameters = null, CancellationToken cancellationToken = default)
    {
        var specialties = await specialtyService.GetAllAsync(parameters, cancellationToken);
        return View(specialties);
    }

    [HttpPost]
    public async Task<IActionResult> Create(AddSpecialtyViewModel viewModel, CancellationToken cancellationToken = default)
    {
        await specialtyService.AddAsync(viewModel, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UpdateSpecialtyViewModel viewModel, CancellationToken cancellationToken = default)
    {
        await specialtyService.UpdateAsync(viewModel, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(string query, CancellationToken cancellationToken = default)
    {
        var results = await specialtyService.SearchByNameAsync(query, cancellationToken);
        return Ok(results.Select(s => new { id = s.Id, text = s.DisplayValue }));
    }
}
