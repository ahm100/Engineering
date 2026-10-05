namespace Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryManagerConfirm;

public record SetRequestMachineryManagerConfirmRequest(List<long> Ids, string? Description) : IHttpRequest;
