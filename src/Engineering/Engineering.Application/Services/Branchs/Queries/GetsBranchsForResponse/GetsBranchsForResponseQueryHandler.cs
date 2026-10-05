using Engineering.Application.Abstractions.Data.Branchs;
using Engineering.Application.Services.Branchs.Models.GetsBranchExcelExporter;

namespace Engineering.Application.Services.Branchs.Queries.GetsBranchsForResponse;

public class GetsBranchsForResponseQueryHandler : IQueryHandler<GetsBranchsForResponseQuery, List<GetsBranchExcelExporterModel>>
{
    private readonly ILogger<GetsBranchsForResponseQueryHandler> _logger;
    private readonly IBranchRepository _repository;

    public GetsBranchsForResponseQueryHandler(
        ILogger<GetsBranchsForResponseQueryHandler> logger,
        IBranchRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<GetsBranchExcelExporterModel>?>> Handle(
        GetsBranchsForResponseQuery request, CT ct)
    {
        try
        {
            var items = await _repository.GetsBranchs(
                request.Ids,
                request.FilterData,
                request.CategoryId,
                request.BranchCode,
                request.BranchName,
                request.IsActive,
                request.OrderBy,
                request.CompanyId,
                request.PageIndex,
                request.PageSize, ct);

            if (items.Data is null || !items.Data.Any())
                return Result.Failure<List<GetsBranchExcelExporterModel>?>(BranchErrors.FilteredBranchNotFound);

            var data = items.Data.Select(e => new GetsBranchExcelExporterModel
            {
                Id = e.Id,
                BranchName = e.BranchName,
                BranchCode = e.BranchCode,
                CategoryId = e.CategoryId,
                CategoryName = e.Category.CategoryName,
                CategoryCode = e.Category.CategoryCode,
                IsActive = e.IsActive,
                CompanyId = e.CompanyId
            }).ToList();

            return data;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<GetsBranchExcelExporterModel>?>(SharedErrors.UnknownError);
        }
    }
}