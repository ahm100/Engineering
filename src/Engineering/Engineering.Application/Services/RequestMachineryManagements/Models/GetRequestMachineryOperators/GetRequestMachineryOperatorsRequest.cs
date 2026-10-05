namespace Engineering.Application.Services.RequestMachineryManagements.Models.GetRequestMachineryOperators;

public record GetRequestMachineryOperatorsRequest(long RequestMachineryId,
                                                  string? FilterData,
                                                  int PageIndex,
                                                  int PageSize) : IHttpRequest;
