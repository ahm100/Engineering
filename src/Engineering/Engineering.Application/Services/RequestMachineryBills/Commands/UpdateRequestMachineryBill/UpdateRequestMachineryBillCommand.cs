using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryBills.Commands.UpdateRequestMachineryBill;

public record UpdateRequestMachineryBillCommand(
        RequestMachineryBill RequestMachineryBill,
        long? ContractorId,
        long? SupplierId,
        long? BillConfirmerId,
        long? DriverId,
        string? DriverName,
        string? NumberPlate,
        string? MachineryAssignment,
        decimal? UnitPrice,
        decimal? TotalPrice,
        DateTime? FromDate,
        DateTime? ToDate,
        long? OperationDuration,
        string? Description) : ICommand<RequestMachineryBill>;
