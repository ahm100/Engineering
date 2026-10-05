using Engineering.Application.Abstractions.Data.RequestContractors;
using Engineering.Domain.Entities.RequestContractors;

namespace Engineering.Application.Services.RequestContractorInquiries.Commands.SetContractorInquiryConfirmedUser;

public class SetContractorInquiryConfirmedUserCommandHandler : ICommandHandler<SetContractorInquiryConfirmedUserCommand, RequestContractorInquiry>
{
    private readonly ILogger<SetContractorInquiryConfirmedUserCommandHandler> _logger;
    private readonly IRequestContractorInquiryRepository _repository;

    public SetContractorInquiryConfirmedUserCommandHandler(ILogger<SetContractorInquiryConfirmedUserCommandHandler> logger, IRequestContractorInquiryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<Result<RequestContractorInquiry?>> Handle(SetContractorInquiryConfirmedUserCommand request, CT ct)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {

        try
        {
            var entity = request.RequestContractorInquiry;

            entity.SetConfirmed(request.ConfirmedUser);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestContractorInquiry>(SharedErrors.UnknownError);
        }
    }
}
