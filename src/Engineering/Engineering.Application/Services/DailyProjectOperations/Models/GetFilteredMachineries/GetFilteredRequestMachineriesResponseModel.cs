using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.DailyProjectOperations.Models.GetFilteredMachineries;

public record GetFilteredRequestMachineriesResponseModel
{
    public long RequestMachineryAssignmentId { get; set; }
    public long RequestMachineryId { get; set; }
    public long? RequestNumber { get; set; }
    public string? RequestFromDate { get; set; }
    public string? RequestToDate { get; set; }
    public string? Contractor { get; set; }
    public RequestMachineryUnit RequestMachineryUnit { get; set; }
    public string? UnitDescription => RequestMachineryUnit.GetEnumDescription();
    public string? MachineryIdentifier { get; set; } = string.Empty;
}