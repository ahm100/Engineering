using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using Engineering.Domain.Entities.FixAssetMachineries;

namespace Engineering.Application.Services.FixAssetMachineries.Commands.CreateFixAssetNotWorkDocument;

public class CreateFixAssetNotWorkDocumentCommandHandler : ICommandHandler<CreateFixAssetNotWorkDocumentCommand, FixAssetNotWorkDocument>
{
    private readonly ILogger<CreateFixAssetNotWorkDocumentCommandHandler> _logger;
    private readonly IFixAssetNotWorkDocumentRepository _repository;

    public CreateFixAssetNotWorkDocumentCommandHandler(ILogger<CreateFixAssetNotWorkDocumentCommandHandler> logger,
                                                              IFixAssetNotWorkDocumentRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<FixAssetNotWorkDocument?>> Handle(CreateFixAssetNotWorkDocumentCommand request, CT ct)
    {
        try
        {
            var entity = new FixAssetNotWorkDocument(request.FixAssetNotWork, request.Url);
            var result = await _repository.Create(entity, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<FixAssetNotWorkDocument>(SharedErrors.UnknownError);
        }
    }
}
