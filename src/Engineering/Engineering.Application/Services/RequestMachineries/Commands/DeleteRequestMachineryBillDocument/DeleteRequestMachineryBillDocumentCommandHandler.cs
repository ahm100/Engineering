using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Commands.DeleteRequestMachineryBillDocument;

public class DeleteRequestMachineryBillDocumentCommandHandler : ICommandHandler<DeleteRequestMachineryBillDocumentCommand, RequestMachineryBillDocument>
{
    private readonly ILogger<DeleteRequestMachineryBillDocumentCommandHandler> _logger;
    private readonly IRequestMachineryBillDocumentRepository _repository;

    public DeleteRequestMachineryBillDocumentCommandHandler(ILogger<DeleteRequestMachineryBillDocumentCommandHandler> logger,
                                                              IRequestMachineryBillDocumentRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachineryBillDocument?>> Handle(DeleteRequestMachineryBillDocumentCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<RequestMachineryBillDocument>(RequestMachineryErrors.RequestMachineryBillDocumentWithIdNotFound);

            entity.SetIsDeleted();

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
