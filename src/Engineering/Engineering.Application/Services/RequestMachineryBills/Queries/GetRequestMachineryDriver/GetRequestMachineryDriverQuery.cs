using Engineering.Application.Services.RequestMachineryBills.Models.GetSupplierDrivers;

namespace Engineering.Application.Services.RequestMachineries.Queries.GetRequestMachineryDriver;

public record GetRequestMachineryDriverQuery(long? requestMachineryId) : IQuery<GetRequestMachineryDriverModel>;
