using Engineering.Application.Abstractions.Data.RequestContractors;
using Engineering.Domain.Entities.RequestContractors;

namespace Engineering.Application.Services.RequestContractorInquiries.Commands.CreateRequestContractorInquiry;

public class CreateRequestContractorInquiryCommandHandler : ICommandHandler<CreateRequestContractorInquiryCommand, RequestContractorInquiry>
{
    private readonly ILogger<CreateRequestContractorInquiryCommandHandler> _logger;
    private readonly IRequestContractorInquiryRepository _repository;

    public CreateRequestContractorInquiryCommandHandler(ILogger<CreateRequestContractorInquiryCommandHandler> logger, IRequestContractorInquiryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestContractorInquiry?>> Handle(CreateRequestContractorInquiryCommand request, CT ct)
    {
        try
        {
            var entity = new RequestContractorInquiry(
                request.ContractorId,
                request.CurrencyId,
                request.TotalAmount,
                request.Amount,
                request.Discount,
                request.Tax,
                request.Type,
                request.FromDate,
                request.ToDate,
                request.Description,
                request.RequestContractor);

            await _repository.Create(entity, ct);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestContractorInquiry>(SharedErrors.UnknownError);
        }
    }
}
