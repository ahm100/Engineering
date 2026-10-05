using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryBills.Commands.DeleteRequestMachineryBill;

public class DeleteRequestMachineryBillCommandHandler : ICommandHandler<DeleteRequestMachineryBillCommand, RequestMachineryBill>
{
    private readonly ILogger<DeleteRequestMachineryBillCommandHandler> _logger;
    private readonly IRequestMachineryBillRepository _repository;

    public DeleteRequestMachineryBillCommandHandler(ILogger<DeleteRequestMachineryBillCommandHandler> logger, IRequestMachineryBillRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachineryBill?>> Handle(DeleteRequestMachineryBillCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.RequestMachineryBillId, ct);
            if (entity is null)
                return Result.Failure<RequestMachineryBill>(RequestMachineryBillErrors.RequestMachineryBillNotFound);
            if (entity.IsDeleted)
                return Result.Failure<RequestMachineryBill>(RequestMachineryBillErrors.IsDeleted);

            entity.SetIsDeleted();

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestMachineryBill>(SharedErrors.UnknownError);
        }
    }
}
