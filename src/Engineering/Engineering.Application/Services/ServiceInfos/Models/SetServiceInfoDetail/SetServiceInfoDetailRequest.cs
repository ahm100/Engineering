namespace Engineering.Application.Services.ServiceInfos.Models.SetServiceInfoDetail;

public record SetServiceInfoDetailRequest(
    long Id,
    string? ServiceInfoEnName,
    string? DescriptionFa,
    string? DescriptionEn
    ) : IHttpRequest;