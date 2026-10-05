namespace Engineering.Application.Services.Machineries.Models.InactiveMachinery;

public record InactiveMachineryRequest(
    long Id
     ) : IHttpRequest;
