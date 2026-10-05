using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryManagements.Commands.DeleteRequestMachineryInquiryDocument;

public class DeleteRequestMachineryInquiryDocumentCommandHandler : ICommandHandler<DeleteRequestMachineryInquiryDocumentCommand, RequestMachineryInquiryDocument>
{
    private readonly ILogger<DeleteRequestMachineryInquiryDocumentCommandHandler> _logger;
    private readonly IRequestMachineryInquiryDocumentRepository _repository;

    public DeleteRequestMachineryInquiryDocumentCommandHandler(ILogger<DeleteRequestMachineryInquiryDocumentCommandHandler> logger,
                                                              IRequestMachineryInquiryDocumentRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachineryInquiryDocument?>> Handle(DeleteRequestMachineryInquiryDocumentCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<RequestMachineryInquiryDocument>(RequestMachineryInquiryDocumentErrors.RequestMachineryInquiryDocumentWithIdNotFound);

            entity.SetIsDeleted();

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestMachineryInquiryDocument>(SharedErrors.UnknownError);
        }
    }
}
