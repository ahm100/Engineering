namespace Engineering.Application.Services.RequestMachineryBills.Models.UpdateRequestMachineryBill;

public record UpdateRequestMachineryBillRequest(
        long Id,
        long? ContractorId,
        long? SupplierId,
        long? BillConfirmerId,
        long? DriverId,
        string? DriverName,
        string? NumberPlates,
        string? MachineryAssignment,
        decimal? UnitPrice,
        DateTime? FromDate,
        DateTime? ToDate,
        string? Description) : IHttpRequest;
