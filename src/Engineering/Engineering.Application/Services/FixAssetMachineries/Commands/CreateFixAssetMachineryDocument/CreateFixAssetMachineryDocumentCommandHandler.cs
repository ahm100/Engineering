using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using Engineering.Domain.Entities.FixAssetMachineries;

namespace Engineering.Application.Services.FixAssetMachineries.Commands.CreateFixAssetMachineryDocument;

public class CreateFixAssetMachineryDocumentCommandHandler : ICommandHandler<CreateFixAssetMachineryDocumentCommand, FixAssetMachineryDocument>
{
    private readonly ILogger<CreateFixAssetMachineryDocumentCommandHandler> _logger;
    private readonly IFixAssetMachineryDocumentRepository _repository;

    public CreateFixAssetMachineryDocumentCommandHandler(ILogger<CreateFixAssetMachineryDocumentCommandHandler> logger,
                                                              IFixAssetMachineryDocumentRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<FixAssetMachineryDocument?>> Handle(CreateFixAssetMachineryDocumentCommand request, CT ct)
    {
        try
        {
            var entity = new FixAssetMachineryDocument(request.FixAssetMachinery, request.Url);
            var result = await _repository.Create(entity, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<FixAssetMachineryDocument>(SharedErrors.UnknownError);
        }
    }
}
