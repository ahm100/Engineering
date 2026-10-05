namespace Engineering.Application.Services.CostOvers.Models.GetsCostOverByNamesOrCodes;

public record GetsCostOverByNamesOrCodesRequest(
    List<string> Names,
    List<string> Codes)
    : IHttpRequest;