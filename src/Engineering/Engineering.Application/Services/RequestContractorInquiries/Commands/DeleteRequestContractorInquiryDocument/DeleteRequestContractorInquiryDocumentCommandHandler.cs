using Engineering.Application.Abstractions.Data.RequestContractors;
using Engineering.Domain.Entities.RequestContractors;

namespace Engineering.Application.Services.RequestContractorInquiries.Commands.DeleteRequestContractorInquiryDocument;

public class DeleteRequestContractorInquiryDocumentCommandHandler : ICommandHandler<DeleteRequestContractorInquiryDocumentCommand, RequestContractorInquiryDocument>
{
    private readonly ILogger<DeleteRequestContractorInquiryDocumentCommandHandler> _logger;
    private readonly IRequestContractorInquiryDocumentRepository _repository;

    public DeleteRequestContractorInquiryDocumentCommandHandler(ILogger<DeleteRequestContractorInquiryDocumentCommandHandler> logger,
                                                              IRequestContractorInquiryDocumentRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestContractorInquiryDocument?>> Handle(DeleteRequestContractorInquiryDocumentCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<RequestContractorInquiryDocument>(RequestContractorInquiryDocumentErrors.RequestContractorInquiryDocumentWithIdNotFound);

            entity.SetIsDeleted();

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestContractorInquiryDocument>(SharedErrors.UnknownError);
        }
    }
}
