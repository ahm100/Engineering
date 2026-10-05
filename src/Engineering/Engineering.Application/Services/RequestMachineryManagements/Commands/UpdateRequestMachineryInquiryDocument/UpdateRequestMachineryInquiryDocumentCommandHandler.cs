using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryManagements.Commands.UpdateRequestMachineryInquiryDocument;

public class UpdateRequestMachineryInquiryDocumentCommandHandler : ICommandHandler<UpdateRequestMachineryInquiryDocumentCommand, RequestMachineryInquiryDocument>
{
    private readonly ILogger<UpdateRequestMachineryInquiryDocumentCommandHandler> _logger;
    private readonly IRequestMachineryInquiryDocumentRepository _repository;

    public UpdateRequestMachineryInquiryDocumentCommandHandler(ILogger<UpdateRequestMachineryInquiryDocumentCommandHandler> logger, IRequestMachineryInquiryDocumentRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachineryInquiryDocument?>> Handle(UpdateRequestMachineryInquiryDocumentCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<RequestMachineryInquiryDocument>(RequestMachineryInquiryDocumentErrors.RequestMachineryInquiryDocumentWithIdNotFound);

            entity.SetUrl(request.Url);

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
