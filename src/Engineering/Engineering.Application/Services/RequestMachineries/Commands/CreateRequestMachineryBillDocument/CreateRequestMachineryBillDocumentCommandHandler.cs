using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Commands.CreateRequestMachineryBillDocument;

public class CreateRequestMachineryBillDocumentCommandHandler : ICommandHandler<CreateRequestMachineryBillDocumentCommand, RequestMachineryBillDocument>
{
    private readonly ILogger<CreateRequestMachineryBillDocumentCommandHandler> _logger;
    private readonly IRequestMachineryBillDocumentRepository _repository;

    public CreateRequestMachineryBillDocumentCommandHandler(ILogger<CreateRequestMachineryBillDocumentCommandHandler> logger,
                                                              IRequestMachineryBillDocumentRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachineryBillDocument?>> Handle(CreateRequestMachineryBillDocumentCommand request, CT ct)
    {
        try
        {
            var entity = new RequestMachineryBillDocument(request.RequestMachinery, request.Url);
            var result = await _repository.Create(entity, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestMachineryBillDocument>(SharedErrors.UnknownError);
        }
    }
}
