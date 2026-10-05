using Engineering.Application.Abstractions.Data.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Commands.DeleteRequestMachineryDocuments;

public class DeleteRequestMachineryDocumentsCommandHandler : ICommandHandler<DeleteRequestMachineryDocumentsCommand, bool?>
{
    private readonly ILogger<DeleteRequestMachineryDocumentsCommandHandler> _logger;
    private readonly IRequestMachineryDocumentRepository _repository;

    public DeleteRequestMachineryDocumentsCommandHandler(ILogger<DeleteRequestMachineryDocumentsCommandHandler> logger,
                                                              IRequestMachineryDocumentRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool?>> Handle(DeleteRequestMachineryDocumentsCommand request, CT ct)
    {
        try
        {
            var entities = await _repository.GetDocumentsByRequestMachineryId(request.Id, ct);
            if (entities is null)
                return Result.Failure<bool?>(RequestMachineryErrors.RequestMachineryDocumentsWithIdNotFound);

            foreach (var item in entities)
            {
                item.SetIsDeleted();
                await _repository.Update(item);
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool?>(SharedErrors.UnknownError);
        }
    }
}
