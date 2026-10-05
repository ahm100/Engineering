namespace Engineering.Application.Services.DailyProjectOperations.Models.GetFilteredMachineries;

public record GetFilteredMachineriesRequest(long ProjectOperationDetailId,
                                            string? FilterData,
                                            string[]? OrderBy,
                                            int PageIndex,
                                            int PageSize) : IHttpRequest;
