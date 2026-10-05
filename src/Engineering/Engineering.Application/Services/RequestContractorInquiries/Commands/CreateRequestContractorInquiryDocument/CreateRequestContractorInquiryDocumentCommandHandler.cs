using Engineering.Application.Abstractions.Data.RequestContractors;
using Engineering.Domain.Entities.RequestContractors;

namespace Engineering.Application.Services.RequestContractorInquiries.Commands.CreateRequestContractorInquiryDocument;

public class CreateRequestContractorInquiryDocumentCommandHandler : ICommandHandler<CreateRequestContractorInquiryDocumentCommand, RequestContractorInquiryDocument>
{
    private readonly ILogger<CreateRequestContractorInquiryDocumentCommandHandler> _logger;
    private readonly IRequestContractorInquiryDocumentRepository _repository;

    public CreateRequestContractorInquiryDocumentCommandHandler(ILogger<CreateRequestContractorInquiryDocumentCommandHandler> logger,
                                                              IRequestContractorInquiryDocumentRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestContractorInquiryDocument?>> Handle(CreateRequestContractorInquiryDocumentCommand request, CT ct)
    {
        try
        {
            var entity = new RequestContractorInquiryDocument(request.Url, request.RequestContractorInquiry);

            var result = await _repository.Create(entity, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestContractorInquiryDocument>(SharedErrors.UnknownError);
        }
    }
}
