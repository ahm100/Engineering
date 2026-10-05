using Engineering.Application.Abstractions.Data.RequestContractors;
using Engineering.Domain.Entities.RequestContractors;
using Engineering.Domain.Entities.RequestContractors.Enums;

namespace Engineering.Application.Services.RequestContractors.Commands.ChangeRequestContractorStatus;

public class ChangeRequestContractorStatusCommandHandler : ICommandHandler<ChangeRequestContractorStatusCommand, RequestContractor>
{
    private readonly ILogger<ChangeRequestContractorStatusCommandHandler> _logger;
    private readonly IRequestContractorRepository _repository;

    public ChangeRequestContractorStatusCommandHandler(ILogger<ChangeRequestContractorStatusCommandHandler> logger, IRequestContractorRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestContractor?>> Handle(ChangeRequestContractorStatusCommand request, CT ct)
    {
        try
        {
            var entity = request.Entity;

            entity.ChangeStatus(request.Status);
            entity.SetStatusDescription(request.Description);

            entity.AddHistory(request.Description);

            if (request.Status == RequestContractorStatus.InquiryRejected)
            {
                entity.ChangeStatus(RequestContractorStatus.Inquiry);
                entity.AddHistory("تغییر وضعیت به صورت سیستمی از رد استعلام ها به استعلام گیری برای ثبت مجدد استعلام انجام شد.");
            }

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestContractor>(SharedErrors.UnknownError);
        }
    }
}
