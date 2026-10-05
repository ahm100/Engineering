using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.UpdateThirdParty;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Commands.UpdateThirdParty;

public class UpdateThirdPartyCommandHandler : ICommandHandler<UpdateThirdPartyCommand, UpdateThirdPartyResponse?>
{
    private readonly ILogger<UpdateThirdPartyCommandHandler> _logger;
    private readonly IMetaDataService _metaDataService;

    public UpdateThirdPartyCommandHandler(ILogger<UpdateThirdPartyCommandHandler> logger,
        IMetaDataService metaDataService)
    {
        _logger = logger;
        _metaDataService = metaDataService;
    }

    public async Task<Result<UpdateThirdPartyResponse?>> Handle(UpdateThirdPartyCommand request,
        CT ct)
    {
        try
        {
            var item = request.Item;
            var thirdParty = request.ThirdParty;
            List<UpdateThirdPartyAddressRequest>? addresses = [];
            if (item.Address != null)
                addresses.Add(new UpdateThirdPartyAddressRequest(item.Address.Id ?? thirdParty?.addresses?.FirstOrDefault()?.Id, item.Address.Title, item.Address.Address,
                    item.Address.PostalCode, null, null, null, item.Address.CityId, null, null, true, true, item.IsDeleted));

            var legal = new UpdateThirdPartyLegalRequest(item.LegalId ?? thirdParty?.legal?.Id, null, item.CompanyName, item.RegistrationNo, null, null, item.LegalIsDeleted);

            var result = await _metaDataService.UpdateThirdParty(new UpdateThirdPartyRequest(
                item.ThirdPartyId > 0 ? item.ThirdPartyId.Value : thirdParty!.Id,
                false, item.FirstName, item.LastName, item.PhoneNumber, null, null,
                null, item.IdentityNo, null, item.IdentityNo, 1, null, null, null, null, item.IsActive,
                null, null, null, null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null, null, null, null,
                item.Description, legal, null, addresses ?? null, null, null, null, null, null,
                null, null, null, null, null), ct);

            if (result.Value is null || result.IsFailure)
                return Result.Failure<UpdateThirdPartyResponse?>(result!.Error ?? SharedErrors.ProviderError);

            return result;
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, ex.Message);
            var response = JsonConvert.DeserializeObject<FailureModel>(ex.Content!);
            response!.Error.StatusCode = 422;
            return Result.Failure<UpdateThirdPartyResponse?>(Gita.Backend.Shared.Domain.Errors.WebServices.MetaDataErrors.ProviderError(response!.Error.Message!));
        }
    }
}