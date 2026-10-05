using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.RemoveThirdParty;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Commands.RemoveThirdParty;

public class RemoveThirdPartyCommandHandler : ICommandHandler<RemoveThirdPartyCommand, RemoveThirdPartyResponse?>
{
    private readonly ILogger<RemoveThirdPartyCommandHandler> _logger;
    private readonly IMetaDataService _metaDataService;

    public RemoveThirdPartyCommandHandler(ILogger<RemoveThirdPartyCommandHandler> logger, IMetaDataService metaDataService)
    {
        _logger = logger;
        _metaDataService = metaDataService;
    }

    public async Task<Result<RemoveThirdPartyResponse?>> Handle(RemoveThirdPartyCommand request, CT ct)
    {
        try
        {
            var result = await _metaDataService.RemoveThirdParty(new RemoveThirdPartyRequest(request.Id), ct);
            if (result.Value is null)
                return Result.Failure<RemoveThirdPartyResponse?>(SharedErrors.ProviderError);

            return result.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RemoveThirdPartyResponse?>(SharedErrors.UnknownError);
        }
    }
}