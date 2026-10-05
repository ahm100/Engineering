using Engineering.Application.Services.ServiceInfos.Models.SetServiceInfoDetail;

namespace Engineering.Application.Services.ServiceInfos.Commands.SetServiceInfoDetail;

public record SetServiceInfoDetailCommand(
    long Id,
    string? ServiceInfoEnName,
    string? DescriptionFa,
    string? DescriptionEn
    ) : ICommand<SetServiceInfoDetailResponse?>;