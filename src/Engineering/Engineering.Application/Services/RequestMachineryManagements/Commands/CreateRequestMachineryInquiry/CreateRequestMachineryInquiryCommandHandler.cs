using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryManagements.Commands.CreateRequestMachineryInquiry;

public class CreateRequestMachineryInquiryCommandHandler : ICommandHandler<CreateRequestMachineryInquiryCommand, RequestMachineryInquiry>
{
    private readonly ILogger<CreateRequestMachineryInquiryCommandHandler> _logger;
    private readonly IRequestMachineryInquiryRepository _repository;

    public CreateRequestMachineryInquiryCommandHandler(ILogger<CreateRequestMachineryInquiryCommandHandler> logger, IRequestMachineryInquiryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachineryInquiry?>> Handle(CreateRequestMachineryInquiryCommand request, CT ct)
    {
        try
        {
            var entity = new RequestMachineryInquiry(request.ThirdPartyId, request.CurrencyId, request.Count, request.Unit, request.UnitPrice, request.TotalPrice, request.Description, request.InquiryRequestedTime, request.RequestMachineryInquiryOperator);

            await _repository.Create(entity, ct);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestMachineryInquiry>(SharedErrors.UnknownError);
        }
    }
}
