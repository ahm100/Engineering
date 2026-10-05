using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Commands.DeleteDailyProjectOperationDocument;

public class DeleteDailyProjectOperationDocumentCommandHandler : ICommandHandler<DeleteDailyProjectOperationDocumentCommand, DailyProjectOperationDocument>
{
    private readonly ILogger<DeleteDailyProjectOperationDocumentCommandHandler> _logger;
    private readonly IDailyProjectOperationDocumentRepository _repository;

    public DeleteDailyProjectOperationDocumentCommandHandler(ILogger<DeleteDailyProjectOperationDocumentCommandHandler> logger,
                                                             IDailyProjectOperationDocumentRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DailyProjectOperationDocument?>> Handle(DeleteDailyProjectOperationDocumentCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.DailyProjectOperationDocumentId, ct);

            if (entity is null)
                return Result.Failure<DailyProjectOperationDocument?>(DailyProjectOperationDocumentErrors.DailyProjectOperationDocumentWithIdNotFound);
            if (entity.IsDeleted)
                return Result.Failure<DailyProjectOperationDocument?>(DailyProjectOperationDocumentErrors.IsDeleted);

            entity.SetIsDeleted();

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DailyProjectOperationDocument?>(SharedErrors.UnknownError);
        }
    }
}
