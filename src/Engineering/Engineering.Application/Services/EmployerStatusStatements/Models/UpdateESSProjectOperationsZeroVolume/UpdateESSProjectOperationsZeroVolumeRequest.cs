
namespace Engineering.Application.Services.EmployerStatusStatements.Models.UpdateESSProjectOperationsZeroVolume;

public record UpdateESSProjectOperationsZeroVolumeRequest(
    List<ESSProjectOperationZeroVolume> ESSProjectOperations
     ) : IHttpRequest;

public record ESSProjectOperationZeroVolume(
    long Id,
    List<string>? Urls,
    string? Description
     );
