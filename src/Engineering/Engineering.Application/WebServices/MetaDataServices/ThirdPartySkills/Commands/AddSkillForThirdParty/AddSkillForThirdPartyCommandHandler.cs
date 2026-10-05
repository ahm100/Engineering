using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.ThirdPartySkills.Models.AddSkillForThirdParty;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdPartySkills.Commands.AddSkillForThirdPartyCommand;
public class AddSkillForThirdPartyCommandHandler : IQueryHandler<AddSkillForThirdPartyCommand, AddSkillForThirdPartyModel?>
{
    private readonly ILogger<AddSkillForThirdPartyCommandHandler> _logger;
    private readonly IMetaDataService _metaDataService;

    public AddSkillForThirdPartyCommandHandler(ILogger<AddSkillForThirdPartyCommandHandler> logger, IMetaDataService metaDataService)
    {
        _logger = logger;
        _metaDataService = metaDataService;
    }

    public async Task<Result<AddSkillForThirdPartyModel?>> Handle(AddSkillForThirdPartyCommand request, CT ct)
    {
        try
        {
            var result = await _metaDataService.AddSkillForThirdParty(request.Adapt<AddSkillForThirdPartyRequest>(), ct);

            return result?.Value != null ? result.Value : Result.Failure<AddSkillForThirdPartyModel?>(SharedErrors.ProviderError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<AddSkillForThirdPartyModel?>(SharedErrors.UnknownError);
        }
    }
}
