namespace Engineering.Application.Services.Machineries.Models.GetMachineryById;

public record GetMachineryByIdRequest(
    long Id
     ) : IHttpRequest;
