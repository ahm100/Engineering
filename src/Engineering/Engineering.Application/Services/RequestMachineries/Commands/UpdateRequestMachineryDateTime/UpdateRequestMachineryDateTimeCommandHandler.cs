using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Commands.UpdateRequestMachineryDateTime;

public class UpdateRequestMachineryDateTimeCommandHandler : ICommandHandler<UpdateRequestMachineryDateTimeCommand, RequestMachinery>
{
    private readonly ILogger<UpdateRequestMachineryDateTimeCommandHandler> _logger;
    private readonly IRequestMachineryRepository _repository;
    private readonly IRequestMachineryInquiryRepository _inquiryRepository;

    public UpdateRequestMachineryDateTimeCommandHandler(
        ILogger<UpdateRequestMachineryDateTimeCommandHandler> logger,
        IRequestMachineryRepository repository,
        IRequestMachineryInquiryRepository inquiryRepository)
    {
        _logger = logger;
        _repository = repository;
        _inquiryRepository = inquiryRepository;
    }

    public async Task<Result<RequestMachinery?>> Handle(UpdateRequestMachineryDateTimeCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetByIdIncludeLess(request.RequestMachineryId, ct);
            if (entity is null)
                return Result.Failure<RequestMachinery>(RequestMachineryErrors.RequestMachineryNotFound);

            var inquiries = entity.InquiryOperators.Where(x => x.Inquiries != null && x.Inquiries.Count > 0).SelectMany(x => x.Inquiries).ToList();
            if (inquiries != null && inquiries.Count > 0)
                foreach (var item in inquiries)
                    if (request.TimeRequired is not null)
                    {
                        item.SetInquiryRequestedTime(request.TimeRequired.Value);
                        item.SetTotalPrice(request.TimeRequired.Value * item.UnitPrice * item.Count);

                        await _inquiryRepository.Update(item);
                    }

            var confirmFromDate = request.FromDate.Add(request.FromTime);
            var confirmToDate = request.ToDate.Add(request.ToTime);

            entity.SetConfirmedTimeRequired(request.TimeRequired);
            entity.SetConfirmFromDate(confirmFromDate);
            entity.SetConfirmToDate(confirmToDate);

            entity.AddHistory(null);

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestMachinery>(SharedErrors.UnknownError);
        }
    }
}
