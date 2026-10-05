using Engineering.Application.Abstractions.Data.RequestContractors;

namespace Engineering.Application.Services.RequestContractorInquiries.Commands.DeleteRequestContractorInquiries;

public class DeleteRequestContractorInquiriesCommandHandler : ICommandHandler<DeleteRequestContractorInquiriesCommand, bool?>
{
    private readonly ILogger<DeleteRequestContractorInquiriesCommandHandler> _logger;
    private readonly IRequestContractorInquiryRepository _repository;

    public DeleteRequestContractorInquiriesCommandHandler(ILogger<DeleteRequestContractorInquiriesCommandHandler> logger, IRequestContractorInquiryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool?>> Handle(DeleteRequestContractorInquiriesCommand request, CT ct)
    {
        try
        {
            var entities = request.RequestContractor.Inquiries;

            if (entities is not null)
                if (entities.Count > 0)
                    foreach (var item in entities)
                    {
                        item.SetIsDeleted();
                        await _repository.Update(item);
                    }

            return true;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool?>(SharedErrors.UnknownError);
        }
    }
}
