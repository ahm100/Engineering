using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using Engineering.Domain.Entities.FixAssetMachineries;

namespace Engineering.Application.Services.FixAssetMachineries.Commands.DeleteFixAssetNotWorkDocument;

public class DeleteFixAssetNotWorkDocumentCommandHandler : ICommandHandler<DeleteFixAssetNotWorkDocumentCommand, FixAssetNotWorkDocument>
{
    private readonly ILogger<DeleteFixAssetNotWorkDocumentCommandHandler> _logger;
    private readonly IFixAssetNotWorkDocumentRepository _repository;

    public DeleteFixAssetNotWorkDocumentCommandHandler(ILogger<DeleteFixAssetNotWorkDocumentCommandHandler> logger,
                                                              IFixAssetNotWorkDocumentRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<FixAssetNotWorkDocument?>> Handle(DeleteFixAssetNotWorkDocumentCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<FixAssetNotWorkDocument>(FixAssetMachineryErrors.DocumentWithIdNotfound);

            entity.SetIsDeleted();

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<FixAssetNotWorkDocument>(SharedErrors.UnknownError);
        }
    }
}
