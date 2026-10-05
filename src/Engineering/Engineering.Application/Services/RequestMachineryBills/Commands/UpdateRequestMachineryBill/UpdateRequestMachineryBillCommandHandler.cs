using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryBills.Commands.UpdateRequestMachineryBill;

public class UpdateRequestMachineryBillCommandHandler : ICommandHandler<UpdateRequestMachineryBillCommand, RequestMachineryBill>
{
    private readonly ILogger<UpdateRequestMachineryBillCommandHandler> _logger;
    private readonly IRequestMachineryBillRepository _repository;

    public UpdateRequestMachineryBillCommandHandler(ILogger<UpdateRequestMachineryBillCommandHandler> logger, IRequestMachineryBillRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachineryBill?>> Handle(UpdateRequestMachineryBillCommand request, CT ct)
    {
        try
        {
            var entity = request.RequestMachineryBill;

            entity.SetDescription(request.Description);
            entity.SetFromDate(request.FromDate);
            entity.SetToDate(request.ToDate);
            entity.SetBillConfirmerId(request.BillConfirmerId);
            entity.SetDriverId(request.DriverId);
            entity.SetDriverName(request.DriverName);
            entity.SetContractorId(request.ContractorId);
            entity.SetNumberPlate(request.NumberPlate);
            entity.SetMachineryAssignment(request.MachineryAssignment);
            entity.SetOperationDuration(request.OperationDuration);
            entity.SetSupplierId(request.SupplierId);
            entity.SetUnitPrice(request.UnitPrice);
            entity.SetTotalPrice(request.TotalPrice);

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
