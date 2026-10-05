using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryManagements.Commands.SetInquiryConfirmedUser;

public class SetInquiryConfirmedUserCommandHandler : ICommandHandler<SetInquiryConfirmedUserCommand, RequestMachineryInquiry>
{
    private readonly ILogger<SetInquiryConfirmedUserCommandHandler> _logger;
    private readonly IRequestMachineryInquiryRepository _repository;

    public SetInquiryConfirmedUserCommandHandler(ILogger<SetInquiryConfirmedUserCommandHandler> logger, IRequestMachineryInquiryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<Result<RequestMachineryInquiry?>> Handle(SetInquiryConfirmedUserCommand request, CT ct)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {

        try
        {
            var entity = request.RequestMachineryInquiry;

            entity.SetConfirmed(request.ConfirmedUser);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestMachineryInquiry>(SharedErrors.UnknownError);
        }
    }
}
