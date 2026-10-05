using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using Engineering.Application.Services.FixAssetMachineries.Models.GetsFixAssetMachineryNotWorkExcelExporter;

namespace Engineering.Application.Services.FixAssetMachineries.Queries.GetsFixAssetMachineryNotWorkForExcel;

public class GetsFixAssetMachineryNotWorkForExcelQueryHandler : IQueryHandler<GetsFixAssetMachineryNotWorkForExcelQuery, DataResult<List<GetsFixAssetMachineryNotWorkExcelExporterModel>>>
{
    private readonly IFixAssetMachineryNotWorkRepository _repository;
    private readonly ILogger<GetsFixAssetMachineryNotWorkForExcelQueryHandler> _logger;

    public GetsFixAssetMachineryNotWorkForExcelQueryHandler(ILogger<GetsFixAssetMachineryNotWorkForExcelQueryHandler> logger, IFixAssetMachineryNotWorkRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsFixAssetMachineryNotWorkExcelExporterModel>>?>> Handle(GetsFixAssetMachineryNotWorkForExcelQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsFixAssetMachineryNotWorkForExcel(
                request.Ids,
                request.NotWorkIds,
                request.MachineryIds,
                request.ContractorIds,
                request.Type,
                request.NumberPlates,
                request.FromDate,
                request.ToDate,
                request.DriverIds,
                request.DriverFilter,
                request.FilterData,
                request.IsActive,
                request.CompanyId,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<GetsFixAssetMachineryNotWorkExcelExporterModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsFixAssetMachineryNotWorkExcelExporterModel>>>(FixAssetMachineryErrors.FilteredFixAssetMachineryNotWorkNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsFixAssetMachineryNotWorkExcelExporterModel>>>(SharedErrors.UnknownError);
        }
    }
}