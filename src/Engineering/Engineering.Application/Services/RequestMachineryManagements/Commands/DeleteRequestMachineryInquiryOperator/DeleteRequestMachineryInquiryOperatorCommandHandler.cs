using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryManagements.Commands.DeleteRequestMachineryInquiryOperator;

public class DeleteRequestMachineryInquiryOperatorCommandHandler : ICommandHandler<DeleteRequestMachineryInquiryOperatorCommand, RequestMachineryInquiryOperator>
{
    private readonly ILogger<DeleteRequestMachineryInquiryOperatorCommandHandler> _logger;
    private readonly IRequestMachineryInquiryOperatorRepository _repository;

    public DeleteRequestMachineryInquiryOperatorCommandHandler(ILogger<DeleteRequestMachineryInquiryOperatorCommandHandler> logger, IRequestMachineryInquiryOperatorRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachineryInquiryOperator?>> Handle(DeleteRequestMachineryInquiryOperatorCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetByIdAsync(request.RequestMachineryInquiryOperatorId, ct);
            if (entity is null)
                return Result.Failure<RequestMachineryInquiryOperator>(RequestMachineryInquiryOperatorErrors.RequestMachineryInquiryOperatorNotFound);
            if (entity.IsDeleted)
                return Result.Failure<RequestMachineryInquiryOperator>(RequestMachineryInquiryOperatorErrors.IsDeleted);
            if (entity.Inquiries.Any())
                return Result.Failure<RequestMachineryInquiryOperator>(RequestMachineryInquiryOperatorErrors.InValidRequestMachineryInquiryOperatorInquiries);

            entity.SetIsDeleted();

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestMachineryInquiryOperator>(SharedErrors.UnknownError);
        }
    }
}
