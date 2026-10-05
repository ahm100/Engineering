using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryBills.Commands.CreateRequestMachineryBill;

public class CreateRequestMachineryBillCommandHandler : ICommandHandler<CreateRequestMachineryBillCommand, RequestMachineryBill>
{
    private readonly ILogger<CreateRequestMachineryBillCommandHandler> _logger;
    private readonly IRequestMachineryBillRepository _repository;

    public CreateRequestMachineryBillCommandHandler(ILogger<CreateRequestMachineryBillCommandHandler> logger, IRequestMachineryBillRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachineryBill?>> Handle(CreateRequestMachineryBillCommand request, CT ct)
    {
        try
        {
            var entity = new RequestMachineryBill(
                request.RequestMachinery,
                request.QRCodeUrl,
                request.BillDate,
                request.ContractorId,
                request.DriverId,
                request.DriverName,
                request.NumberPlate,
                request.MachineryAssignment,
                request.FromDate,
                request.ToDate,
                request.OperationDuration,
                request.Description,
                request.SupplierId,
                request.UnitPrice,
                request.TotalPrice,
                request.BillConfirmerId);

            var result = await _repository.Create(entity, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestMachineryBill>(SharedErrors.UnknownError);
        }
    }
}
