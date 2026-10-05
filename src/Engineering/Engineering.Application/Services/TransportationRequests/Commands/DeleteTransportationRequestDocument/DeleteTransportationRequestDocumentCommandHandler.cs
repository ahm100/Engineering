using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Application.Services.TransportationRequests.Commands.DeleteTransportationRequestDocument;

public class DeleteTransportationRequestDocumentCommandHandler : ICommandHandler<
    DeleteTransportationRequestDocumentCommand, TransportationRequestDocument>
{
    private readonly ILogger<DeleteTransportationRequestDocumentCommandHandler> _logger;
    private readonly ITransportationRequestDocumentRepository _repository;

    public DeleteTransportationRequestDocumentCommandHandler(
        ILogger<DeleteTransportationRequestDocumentCommandHandler> logger,
        ITransportationRequestDocumentRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<TransportationRequestDocument?>> Handle(DeleteTransportationRequestDocumentCommand request,
        CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.TransportationRequestDocumentId, ct);

            if (entity is null)
                return Result.Failure<TransportationRequestDocument?>(TransportationRequestDocumentErrors
                    .TransportationRequestDocumentWithIdNotFound);
            if (entity.IsDeleted)
                return Result.Failure<TransportationRequestDocument?>(TransportationRequestDocumentErrors.IsDeleted);

            entity.SetIsDeleted();

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<TransportationRequestDocument?>(SharedErrors.UnknownError);
        }
    }
}