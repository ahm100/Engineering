namespace Engineering.Application.Services.Machineries.Models.ActiveMachinery;

public record ActiveMachineryRequest(
    long Id
     ) : IHttpRequest;
