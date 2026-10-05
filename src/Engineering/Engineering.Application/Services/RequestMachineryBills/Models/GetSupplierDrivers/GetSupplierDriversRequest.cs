namespace Engineering.Application.Services.RequestMachineryBills.Models.GetSupplierDrivers;

public record GetSupplierDriversRequest(long? RequestMachineryId,
                                        long? SupplierId,
                                        string? FilterData) : IHttpRequest;
