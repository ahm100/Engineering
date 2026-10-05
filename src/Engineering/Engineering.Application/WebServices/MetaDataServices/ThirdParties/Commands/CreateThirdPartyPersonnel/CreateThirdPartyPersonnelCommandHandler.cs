using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.CreateThirdParty;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Commands.CreateThirdPartyPersonnel;

public class CreateThirdPartyPersonnelCommandHandler : ICommandHandler<CreateThirdPartyPersonnelCommand, CreateThirdPartyResponse?>
{
    private readonly ILogger<CreateThirdPartyPersonnelCommandHandler> _logger;
    private readonly IMetaDataService _metaDataService;

    public CreateThirdPartyPersonnelCommandHandler(ILogger<CreateThirdPartyPersonnelCommandHandler> logger, IMetaDataService metaDataService)
    {
        _logger = logger;
        _metaDataService = metaDataService;
    }

    public async Task<Result<CreateThirdPartyResponse?>> Handle(CreateThirdPartyPersonnelCommand request, CT ct)
    {
        try
        {
            var item = request.Item;

            List<CreateThirdPartyAddressRequest>? addresses = [];
            if (item.PersonnelAddress != null)
                addresses.Add(new CreateThirdPartyAddressRequest(item.PersonnelAddress.Title, item.PersonnelAddress.Address,
                    null, null, null, null, item.PersonnelAddress.CityId, null, null, true, true));

            List<CreateThirdPartyCompanyRequest>? companies = [];
            if (request.CompanyId != null)
                companies.Add(new CreateThirdPartyCompanyRequest(request.CompanyId!.Value, true));

            var result = await _metaDataService.CreateThirdParty(new CreateThirdPartyRequest(
                true, item.FirstName, item.LastName, item.PhoneNumber, null,
                null, null, null, 1, null, null, item.Description, null, null,
                item.IdentityNo, null, true, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, addresses ?? null, null, null, null,
                null, companies ?? null, null, null, null, null, null),
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