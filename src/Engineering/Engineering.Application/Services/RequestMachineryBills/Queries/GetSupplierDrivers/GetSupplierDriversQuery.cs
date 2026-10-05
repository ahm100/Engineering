namespace Engineering.Application.Services.RequestMachineryBills.Queries.GetSupplierDrivers;

public record GetSupplierDriversQuery(
        long? SupplierId,
        string? FilterData) : IQuery<List<string?>?>;
