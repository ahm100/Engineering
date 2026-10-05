using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.Banks.Models.GetBankById;
using BankModel = Engineering.Application.WebServices.MetaDataServices.Banks.Models.Bank;

namespace Engineering.Application.WebServices.MetaDataServices.Banks.Queries.GetBankById;

public class GetBankByIdQueryHandler : IQueryHandler<GetBankByIdQuery, BankModel?>
{
    private readonly ILogger<GetBankByIdQueryHandler> _logger;
    private readonly IMetaDataService _metaDataService;

    public GetBankByIdQueryHandler(ILogger<GetBankByIdQueryHandler> logger, IMetaDataService metaDataService)
    {
        _logger = logger;
        _metaDataService = metaDataService;
    }

    public async Task<Result<BankModel?>> Handle(GetBankByIdQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetBankById(request.Adapt<GetBankByIdRequest>(), ct);

            return result?.Value ?? Result.Failure<BankModel?>(SharedErrors.ProviderError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<BankModel?>(SharedErrors.UnknownError);
        }
    }
}
