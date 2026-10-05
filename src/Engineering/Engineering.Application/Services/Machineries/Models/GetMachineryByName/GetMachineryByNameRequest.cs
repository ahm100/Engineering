namespace Engineering.Application.Services.Machineries.Models.GetMachineryByName;

public record GetMachineryByNameRequest(
    string MachineryName
     ) : IHttpRequest;
