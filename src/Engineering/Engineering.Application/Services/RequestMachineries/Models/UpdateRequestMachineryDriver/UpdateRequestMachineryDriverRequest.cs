namespace Engineering.Application.Services.RequestMachineries.Models.UpdateRequestMachineryDriver;

public record UpdateRequestMachineryDriverRequest(long RequestMachineryId,
                                                  long? DriverId,
                                                  string? DriverName) : IHttpRequest;
