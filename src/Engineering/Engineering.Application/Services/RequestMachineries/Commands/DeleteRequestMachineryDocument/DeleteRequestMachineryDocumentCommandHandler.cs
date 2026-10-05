using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Commands.DeleteRequestMachineryDocument;

public class DeleteRequestMachineryDocumentCommandHandler : ICommandHandler<DeleteRequestMachineryDocumentCommand, RequestMachineryDocument>
{
    private readonly ILogger<DeleteRequestMachineryDocumentCommandHandler> _logger;
    private readonly IRequestMachineryDocumentRepository _repository;

    public DeleteRequestMachineryDocumentCommandHandler(ILogger<DeleteRequestMachineryDocumentCommandHandler> logger,
                                                              IRequestMachineryDocumentRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachineryDocument?>> Handle(DeleteRequestMachineryDocumentCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<RequestMachineryDocument>(RequestMachineryErrors.RequestMachineryDocumentWithIdNotFound);

            entity.SetIsDeleted();

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestMachineryDocument>(SharedErrors.UnknownError);
        }
    }
}
