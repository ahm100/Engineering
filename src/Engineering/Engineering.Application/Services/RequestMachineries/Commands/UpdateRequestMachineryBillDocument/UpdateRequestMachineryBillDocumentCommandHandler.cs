using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Commands.UpdateRequestMachineryBillDocument;

public class UpdateRequestMachineryBillDocumentCommandHandler : ICommandHandler<UpdateRequestMachineryBillDocumentCommand, RequestMachineryBillDocument>
{
    private readonly ILogger<UpdateRequestMachineryBillDocumentCommandHandler> _logger;
    private readonly IRequestMachineryBillDocumentRepository _repository;

    public UpdateRequestMachineryBillDocumentCommandHandler(ILogger<UpdateRequestMachineryBillDocumentCommandHandler> logger, IRequestMachineryBillDocumentRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachineryBillDocument?>> Handle(UpdateRequestMachineryBillDocumentCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<RequestMachineryBillDocument>(RequestMachineryErrors.RequestMachineryBillDocumentWithIdNotFound);

            entity.SetUrl(request.Url);

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestMachineryBillDocument>(SharedErrors.UnknownError);
        }
    }
}
