using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryManagements.Commands.UpdateRequestMachineryInquiry;

public class UpdateRequestMachineryInquiryCommandHandler : ICommandHandler<UpdateRequestMachineryInquiryCommand, RequestMachineryInquiry>
{
    private readonly ILogger<UpdateRequestMachineryInquiryCommandHandler> _logger;
    private readonly IRequestMachineryInquiryRepository _repository;

    public UpdateRequestMachineryInquiryCommandHandler(ILogger<UpdateRequestMachineryInquiryCommandHandler> logger, IRequestMachineryInquiryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachineryInquiry?>> Handle(UpdateRequestMachineryInquiryCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.RequestMachineryInquiryId, ct);
            if (entity is null)
                return Result.Failure<RequestMachineryInquiry>(RequestMachineryInquiryErrors.RequestMachineryInquiryNotFOund);

            entity.SetThirdPartyId(request.ThirdPartyId);
            entity.SetCount(request.Count);
            entity.SetUnit(request.Unit);
            entity.SetUnitPrice(request.UnitPrice);
            entity.SetTotalPrice(request.TotalPrice);
            entity.SetCurrencyId(request.CurrencyId);
            entity.SetDescription(request.Description);
            entity.SetInquiryRequestedTime(request.InquiryRequestedTime);

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestMachineryInquiry>(SharedErrors.UnknownError);
        }
    }
}
