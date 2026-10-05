namespace Engineering.Application.Services.RequestMachineries.Models.GetRequestMachineryMachineries;

public record GetRequestMachineryMachineriesRequest(long RequestMachineryId,
                                                    string? FilterData,
                                                    int PageIndex,
                                                    int PageSize) : IHttpRequest;
