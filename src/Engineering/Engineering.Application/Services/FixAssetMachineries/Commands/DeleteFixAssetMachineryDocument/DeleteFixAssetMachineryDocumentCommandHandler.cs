using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using Engineering.Domain.Entities.FixAssetMachineries;

namespace Engineering.Application.Services.FixAssetMachineries.Commands.DeleteFixAssetMachineryDocument;

public class DeleteFixAssetMachineryDocumentCommandHandler : ICommandHandler<DeleteFixAssetMachineryDocumentCommand, FixAssetMachineryDocument>
{
    private readonly ILogger<DeleteFixAssetMachineryDocumentCommandHandler> _logger;
    private readonly IFixAssetMachineryDocumentRepository _repository;

    public DeleteFixAssetMachineryDocumentCommandHandler(ILogger<DeleteFixAssetMachineryDocumentCommandHandler> logger,
                                                              IFixAssetMachineryDocumentRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<FixAssetMachineryDocument?>> Handle(DeleteFixAssetMachineryDocumentCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<FixAssetMachineryDocument>(FixAssetMachineryErrors.DocumentWithIdNotfound);

            entity.SetIsDeleted();

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<FixAssetMachineryDocument>(SharedErrors.UnknownError);
        }
    }
}
