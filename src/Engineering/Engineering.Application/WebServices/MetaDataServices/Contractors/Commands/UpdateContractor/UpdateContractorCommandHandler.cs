using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.Contractors.Models;
using Engineering.Application.WebServices.MetaDataServices.Contractors.Models.UpdateContractor;

namespace Engineering.Application.WebServices.MetaDataServices.Contractors.Commands.UpdateContractor;

public class UpdateContractorCommandHandler : ICommandHandler<UpdateContractorCommand, Contractor?>
{
    private readonly ILogger<UpdateContractorCommandHandler> _logger;
    private readonly IMetaDataService _metaDataService;

    public UpdateContractorCommandHandler(ILogger<UpdateContractorCommandHandler> logger, IMetaDataService metaDataService)
    {
        _logger = logger;
        _metaDataService = metaDataService;
    }

    public async Task<Result<Contractor?>> Handle(UpdateContractorCommand request, CT ct)
    {
        try
        {
            var result = await _metaDataService.UpdateContractor(request.Adapt<UpdateContractorRequest>(), ct);

            return result?.Value ?? Result.Failure<Contractor?>(SharedErrors.ProviderError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Contractor?>(SharedErrors.UnknownError);
        }
    }
}
