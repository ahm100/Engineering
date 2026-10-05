using Engineering.Application.Abstractions.Data.RequestContractors;
using Engineering.Domain.Entities.RequestContractors;

namespace Engineering.Application.Services.RequestContractorInquiries.Commands.UpdateRequestContractorInquiry;

public class UpdateRequestContractorInquiryCommandHandler : ICommandHandler<UpdateRequestContractorInquiryCommand, RequestContractorInquiry>
{
    private readonly ILogger<UpdateRequestContractorInquiryCommandHandler> _logger;
    private readonly IRequestContractorInquiryRepository _repository;

    public UpdateRequestContractorInquiryCommandHandler(ILogger<UpdateRequestContractorInquiryCommandHandler> logger, IRequestContractorInquiryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestContractorInquiry?>> Handle(UpdateRequestContractorInquiryCommand request, CT ct)
    {
        try
        {
            var entity = request.RequestContractorInquiry;
            if (entity is null)
                return Result.Failure<RequestContractorInquiry>(RequestContractorInquiryErrors.RequestContractorInquiryNotFound);

            entity.SetContractorId(request.ContractorId);
            entity.SetCurrencyId(request.CurrencyId);
            entity.SetAmount(request.Amount);
            entity.SetTotalAmount(request.TotalAmount);
            entity.SetDiscount(request.Discount);
            entity.SetTax(request.Tax);
            entity.SetFromDate(request.FromDate);
            entity.SetToDate(request.ToDate);
            entity.SetType(request.Type);
            entity.SetDescription(request.Description);

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestContractorInquiry>(SharedErrors.UnknownError);
        }
    }
}
