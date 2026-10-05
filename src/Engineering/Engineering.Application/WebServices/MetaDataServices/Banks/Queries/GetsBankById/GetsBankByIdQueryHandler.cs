using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.Banks.Models.GetsBankById;
using BankModel = Engineering.Application.WebServices.MetaDataServices.Banks.Models.Bank;

namespace Engineering.Application.WebServices.MetaDataServices.Banks.Queries.GetsBankById;

public class GetsBankByIdQueryHandler : IQueryHandler<GetsBankByIdQuery, DataResult<List<BankModel>>>
{
    private readonly IMetaDataService _metaDataService;
    private readonly ILogger<GetsBankByIdQueryHandler> _logger;

    public GetsBankByIdQueryHandler(ILogger<GetsBankByIdQueryHandler> logger, IMetaDataService repository)
    {
        _logger = logger;
        _metaDataService = repository;
    }

    public async Task<Result<DataResult<List<BankModel>>?>> Handle(GetsBankByIdQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetsBankById(request.Adapt<GetsBankByIdRequest>(), ct);

            return (result?.Value?.Data?.Any()) ?? false ?
                new DataResult<List<BankModel>>
                {
                    Data = result.Value.Data,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<BankModel>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<BankModel>>>(SharedErrors.UnknownError);
        }
    }
}
