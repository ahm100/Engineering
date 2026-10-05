using Engineering.Application.Abstractions.Data.RequestContractors;
using Engineering.Domain.Entities.RequestContractors;

namespace Engineering.Application.Services.RequestContractorInquiries.Commands.DeleteRequestContractorInquiry;

public class DeleteRequestContractorInquiryCommandHandler : ICommandHandler<DeleteRequestContractorInquiryCommand, RequestContractorInquiry>
{
    private readonly ILogger<DeleteRequestContractorInquiryCommandHandler> _logger;
    private readonly IRequestContractorInquiryRepository _repository;

    public DeleteRequestContractorInquiryCommandHandler(ILogger<DeleteRequestContractorInquiryCommandHandler> logger, IRequestContractorInquiryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestContractorInquiry?>> Handle(DeleteRequestContractorInquiryCommand request, CT ct)
    {
        try
        {
            var entity = request.RequestContractorInquiry;
            if (entity.IsDeleted)
                return Result.Failure<RequestContractorInquiry>(RequestContractorInquiryErrors.IsDeleted);

            entity.SetIsDeleted();

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
