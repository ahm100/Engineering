using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryManagements.Commands.DeleteRequestMachineryInquiry;

public class DeleteRequestMachineryInquiryCommandHandler : ICommandHandler<DeleteRequestMachineryInquiryCommand, RequestMachineryInquiry>
{
    private readonly ILogger<DeleteRequestMachineryInquiryCommandHandler> _logger;
    private readonly IRequestMachineryInquiryRepository _repository;

    public DeleteRequestMachineryInquiryCommandHandler(ILogger<DeleteRequestMachineryInquiryCommandHandler> logger, IRequestMachineryInquiryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachineryInquiry?>> Handle(DeleteRequestMachineryInquiryCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.RequestMachineryInquiryId, ct);
            if (entity is null)
                return Result.Failure<RequestMachineryInquiry>(RequestMachineryInquiryErrors.RequestMachineryInquiryNotFOund);
            if (entity.IsDeleted)
                return Result.Failure<RequestMachineryInquiry>(RequestMachineryInquiryErrors.IsDeleted);

            entity.SetIsDeleted();

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
