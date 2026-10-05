using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.CreateThirdParty;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Commands.CreateThirdParty;

public class CreateThirdPartyCommandHandler : ICommandHandler<CreateThirdPartyCommand, CreateThirdPartyResponse?>
{
    private readonly ILogger<CreateThirdPartyCommandHandler> _logger;
    private readonly IMetaDataService _metaDataService;

    public CreateThirdPartyCommandHandler(ILogger<CreateThirdPartyCommandHandler> logger, IMetaDataService metaDataService)
    {
        _logger = logger;
        _metaDataService = metaDataService;
    }

    public async Task<Result<CreateThirdPartyResponse?>> Handle(CreateThirdPartyCommand request, CT ct)
    {
        try
        {
            var item = request.Item;

            List<CreateThirdPartyAddressRequest>? addresses = [];
            if (item.Address != null)
                addresses.Add(new CreateThirdPartyAddressRequest(item.Address.Title, item.Address.Address,
                    item.Address.PostalCode, null, null, null, item.Address.CityId, null, null, true, true));

            var legal = new CreateThirdPartyLegalRequest(null, item.CompanyName, item.RegistrationNo, null, null);

            List<CreateThirdPartyCompanyRequest>? companies = [];
            if (request.CompanyId != null)
                companies.Add(new CreateThirdPartyCompanyRequest(request.CompanyId!.Value, true));

            var result = await _metaDataService.CreateThirdParty(new CreateThirdPartyRequest(
                false, item.FirstName!, item.LastName!, item.PhoneNumber!, null,
                null, null, item.IdentityNo, null, null, null, item.Description, null, null,
                item.IdentityNo, null, item.IsActive, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, legal, null, addresses ?? null, null, null, null, null, companies ?? null,
                null, null, null, null, null),
                ct);

            if (result.IsFailure || result.Value is null)
                return Result.Failure<CreateThirdPartyResponse?>(result!.Error ?? SharedErrors.ProviderError);

            return result;
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, ex.Message);
            var response = JsonConvert.DeserializeObject<FailureModel>(ex.Content!);
            if (response!.Error != null)
                response!.Error.StatusCode = 422;
            return Result.Failure<CreateThirdPartyResponse?>(Gita.Backend.Shared.Domain.Errors.WebServices.MetaDataErrors.ProviderError(response!.Error?.Message!));
        }
    }
}