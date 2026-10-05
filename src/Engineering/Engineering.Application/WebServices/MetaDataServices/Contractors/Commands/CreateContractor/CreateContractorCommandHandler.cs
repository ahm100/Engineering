using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.Contractors.Models;
using Engineering.Application.WebServices.MetaDataServices.Contractors.Models.CreateContractor;

namespace Engineering.Application.WebServices.MetaDataServices.Contractors.Commands.CreateContractor;

public class CreateContractorCommandHandler : ICommandHandler<CreateContractorCommand, Contractor?>
{
    private readonly ILogger<CreateContractorCommandHandler> _logger;
    private readonly IMetaDataService _metaDataService;

    public CreateContractorCommandHandler(ILogger<CreateContractorCommandHandler> logger, IMetaDataService metaDataService)
    {
        _logger = logger;
        _metaDataService = metaDataService;
    }

    public async Task<Result<Contractor?>> Handle(CreateContractorCommand request, CT ct)
    {
        try
        {
            var result = await _metaDataService.CreateContractor(request.Adapt<CreateContractorRequest>(), ct);

            return result?.Value ?? Result.Failure<Contractor?>(SharedErrors.ProviderError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Contractor?>(SharedErrors.UnknownError);
        }
    }
}
