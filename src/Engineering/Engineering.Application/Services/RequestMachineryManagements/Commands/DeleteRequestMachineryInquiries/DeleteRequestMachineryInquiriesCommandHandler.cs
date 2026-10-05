using Engineering.Application.Abstractions.Data.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryManagements.Commands.DeleteRequestMachineryInquiries;

public class DeleteRequestMachineryInquiriesCommandHandler : ICommandHandler<DeleteRequestMachineryInquiriesCommand, bool?>
{
    private readonly ILogger<DeleteRequestMachineryInquiriesCommandHandler> _logger;
    private readonly IRequestMachineryInquiryRepository _repository;

    public DeleteRequestMachineryInquiriesCommandHandler(ILogger<DeleteRequestMachineryInquiriesCommandHandler> logger, IRequestMachineryInquiryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool?>> Handle(DeleteRequestMachineryInquiriesCommand request, CT ct)
    {
        try
        {
            var entities = request.RequestMachinery.InquiryOperators
                .Where(x => x.Inquiries is not null && x.Inquiries.Count > 0)
                .SelectMany(x => x.Inquiries).ToList();

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
