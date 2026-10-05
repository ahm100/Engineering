using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryManagements.Commands.ConfirmRequestMachineryInquiry;

public class ConfirmRequestMachineryInquiryCommandHandler : ICommandHandler<ConfirmRequestMachineryInquiryCommand, RequestMachineryInquiry>
{
    private readonly ILogger<ConfirmRequestMachineryInquiryCommandHandler> _logger;
    private readonly IRequestMachineryInquiryRepository _repository;

    public ConfirmRequestMachineryInquiryCommandHandler(ILogger<ConfirmRequestMachineryInquiryCommandHandler> logger, IRequestMachineryInquiryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachineryInquiry?>> Handle(ConfirmRequestMachineryInquiryCommand request, CT ct)
    {

        try
        {
            var entity = await _repository.GetByIdAsync(request.RequestMachineryInquiryId, request.RequestMachineryId, ct);
            if (entity is null)
                return Result.Failure<RequestMachineryInquiry>(RequestMachineryInquiryErrors.RequestMachineryInquiryNotFOund);

            entity.SetConfirmed(request.ConfirmedUser);

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
