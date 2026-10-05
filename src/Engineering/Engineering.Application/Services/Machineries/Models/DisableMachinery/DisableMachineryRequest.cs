namespace Engineering.Application.Services.Machineries.Models.DisableMachinery;

public record DisableMachineryRequest(
    long Id
     ) : IHttpRequest;
