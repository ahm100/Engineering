namespace Engineering.Application.Services.MachineriesGroups.Models.GetMachineriesGroupByCode;

public record GetMachineriesGroupByCodeRequest(
    string GroupCode
     ) : IHttpRequest;
