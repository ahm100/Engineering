
namespace Engineering.Application.Services.EmployerStatusStatements.Models.UpdateESSProjectOperationDetailDailyVolume;

public record UpdateESSProjectOperationDetailDailyVolumeRequest(
    List<ESSProjectOperationDetailDailyVolume> ESSProjectOperationDailies
     ) : IHttpRequest;

public record ESSProjectOperationDetailDailyVolume(
    long Id,
    List<string>? Urls,
    decimal Length,
    decimal Width,
    decimal Height,
    decimal Weight,
    decimal Number,
    string? Description
     );
