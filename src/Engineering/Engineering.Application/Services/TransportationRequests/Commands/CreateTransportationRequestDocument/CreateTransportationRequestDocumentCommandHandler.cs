using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Application.Services.TransportationRequests.Commands.CreateTransportationRequestDocument;

public class CreateTransportationRequestDocumentCommandHandler : ICommandHandler<CreateTransportationRequestDocumentCommand, TransportationRequestDocument>
{
    private readonly ILogger<CreateTransportationRequestDocumentCommandHandler> _logger;
    private readonly ITransportationRequestDocumentRepository _repository;

    public CreateTransportationRequestDocumentCommandHandler(ILogger<CreateTransportationRequestDocumentCommandHandler> logger,
                                                             ITransportationRequestDocumentRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<TransportationRequestDocument?>> Handle(CreateTransportationRequestDocumentCommand request, CT ct)
    {
        try
        {
            var entity = new TransportationRequestDocument(request.Url, false, request.TransportationRequest);

            var result = await _repository.Create(entity, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<TransportationRequestDocument?>(SharedErrors.UnknownError);
        }
    }
}
