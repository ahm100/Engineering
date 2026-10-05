namespace Engineering.Application.Services.Machineries.Models.GetMachineryByCode;

public record GetMachineryByCodeRequest(
    string MachineryCode
     ) : IHttpRequest;
