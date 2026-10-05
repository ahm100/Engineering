namespace Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachinerySendToManager;

public record SetRequestMachinerySendToManagerRequest(List<long> Ids, string? Description) : IHttpRequest;
