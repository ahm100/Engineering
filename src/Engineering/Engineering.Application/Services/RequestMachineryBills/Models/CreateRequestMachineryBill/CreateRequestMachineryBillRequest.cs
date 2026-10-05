namespace Engineering.Application.Services.RequestMachineryBills.Models.CreateRequestMachineryBill;

public record CreateRequestMachineryBillRequest(
        long RequestMachineryId,
        long? ContractorId,
        long? SupplierId,
        long? BillConfirmerId,
        long? DriverId,
        string? DriverName,
        string? NumberPlates,
        string? MachineryAssignment,
        string? QRCodeUrl,
        decimal? UnitPrice,
        DateTime FromDate,
        DateTime ToDate,
        string? Description) : IHttpRequest;
