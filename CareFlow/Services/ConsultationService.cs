using CareFlow.Data;
using CareFlow.Extensions;
using CareFlow.Mappers;
using CareFlow.Models.Results;
using CareFlow.Services.Interfaces;
using CareFlow.ViewModels.Consultations;
using Microsoft.EntityFrameworkCore;

namespace CareFlow.Services;

public class ConsultationService(ApplicationDbContext dbContext) : IConsultationService
{
    public async Task<PagedResult<ConsultationViewModel>> GetAllAsync
        (PaginationParameters? parameters = null, CancellationToken cancellationToken = default)
    {
        parameters ??= new PaginationParameters();
        return await dbContext.Consultations
            .AsNoTracking()
            .Select(ConsultationMappers.ToViewModelExpression())
            .ToPagedResultAsync(parameters, cancellationToken);
    }
}
