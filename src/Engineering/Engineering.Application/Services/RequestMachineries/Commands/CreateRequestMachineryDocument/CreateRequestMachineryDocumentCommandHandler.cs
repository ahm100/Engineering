using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Commands.CreateRequestMachineryDocument;

public class CreateRequestMachineryDocumentCommandHandler : ICommandHandler<CreateRequestMachineryDocumentCommand, RequestMachineryDocument>
{
    private readonly ILogger<CreateRequestMachineryDocumentCommandHandler> _logger;
    private readonly IRequestMachineryDocumentRepository _repository;

    public CreateRequestMachineryDocumentCommandHandler(ILogger<CreateRequestMachineryDocumentCommandHandler> logger,
                                                              IRequestMachineryDocumentRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachineryDocument?>> Handle(CreateRequestMachineryDocumentCommand request, CT ct)
    {
        try
        {
            var entity = new RequestMachineryDocument(request.RequestMachinery, request.Url);

            var result = await _repository.Create(entity, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestMachineryDocument>(SharedErrors.UnknownError);
        }
    }
}
