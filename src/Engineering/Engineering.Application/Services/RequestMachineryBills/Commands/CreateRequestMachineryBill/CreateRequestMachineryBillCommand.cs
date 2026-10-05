using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryBills.Commands.CreateRequestMachineryBill;

public record CreateRequestMachineryBillCommand(
        RequestMachinery RequestMachinery,
        DateTime BillDate,
        long? ContractorId,
        long? SupplierId,
        long? BillConfirmerId,
        long? DriverId,
        string? DriverName,
        string? NumberPlate,
        string? MachineryAssignment,
        string? QRCodeUrl,
        decimal? UnitPrice,
        decimal? TotalPrice,
        DateTime? FromDate,
        DateTime? ToDate,
        long? OperationDuration,
        string? Description) : ICommand<RequestMachineryBill>;
