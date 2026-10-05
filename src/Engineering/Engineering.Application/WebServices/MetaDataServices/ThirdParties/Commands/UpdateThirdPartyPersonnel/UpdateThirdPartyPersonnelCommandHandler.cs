using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Commands.UpdateThirdParty;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.UpdateThirdParty;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Commands.UpdateThirdPartyPersonnel;

public class UpdateThirdPartyPersonnelCommandHandler : ICommandHandler<UpdateThirdPartyPersonnelCommand, UpdateThirdPartyResponse?>
{
    private readonly ILogger<UpdateThirdPartyPersonnelCommandHandler> _logger;
    private readonly IMetaDataService _metaDataService;

    public UpdateThirdPartyPersonnelCommandHandler(ILogger<UpdateThirdPartyPersonnelCommandHandler> logger,
        IMetaDataService metaDataService)
    {
        _logger = logger;
        _metaDataService = metaDataService;
    }

    public async Task<Result<UpdateThirdPartyResponse?>> Handle(UpdateThirdPartyPersonnelCommand request,
        CT ct)
    {
        try
        {
            var item = request.Item;
            List<UpdateThirdPartyAddressRequest>? addresses = [];
            if (item.PersonnelAddress != null)
                addresses.Add(new UpdateThirdPartyAddressRequest(item.PersonnelAddress.Id, item.PersonnelAddress.Title, item.PersonnelAddress.Address,
                    null, null, null, null, item.PersonnelAddress.CityId, null, null, true, true, item.IsDeleted));

            var result = await _metaDataService.UpdateThirdParty(new UpdateThirdPartyRequest(
                item.ThirdPartyId!.Value, true, item.FirstName, item.LastName, item.PhoneNumber,
                null, null, null, null, null, item.IdentityNo, 1, null, null, null, null,
                item.IsActive, null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null, null, null, null,
                null, null, null, item.Description, null, null, addresses ?? null, null, null,
                null, null, null, null, null, null, null, null), ct);

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