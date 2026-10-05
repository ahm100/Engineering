using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryManagements.Commands.CreateRequestMachineryInquiryDocument;

public class CreateRequestMachineryInquiryDocumentCommandHandler : ICommandHandler<CreateRequestMachineryInquiryDocumentCommand, RequestMachineryInquiryDocument>
{
    private readonly ILogger<CreateRequestMachineryInquiryDocumentCommandHandler> _logger;
    private readonly IRequestMachineryInquiryDocumentRepository _repository;

    public CreateRequestMachineryInquiryDocumentCommandHandler(ILogger<CreateRequestMachineryInquiryDocumentCommandHandler> logger,
                                                              IRequestMachineryInquiryDocumentRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachineryInquiryDocument?>> Handle(CreateRequestMachineryInquiryDocumentCommand request, CT ct)
    {
        try
        {
            var entity = new RequestMachineryInquiryDocument(request.Url, request.RequestMachineryInquiry);

            var result = await _repository.Create(entity, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestMachineryInquiryDocument>(SharedErrors.UnknownError);
        }
    }
}
