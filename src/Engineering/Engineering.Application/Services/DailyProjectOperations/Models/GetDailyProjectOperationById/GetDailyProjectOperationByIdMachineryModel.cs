using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationById;

public record GetDailyProjectOperationByIdMachineryModel
{
    public long ConsumableVolumeMachineryId { get; set; }
    public long MachineryId { get; set; }
    public long MachineryGroupId { get; set; }
    public string MachineryCode { get; set; } = string.Empty;
    public string MachineryName { get; set; } = string.Empty;
    public string MachineryGroupName { get; set; } = string.Empty;
    public string FinalValue { get; set; } = string.Empty;
    public List<GetDailyProjectOperationByIdRequestMachineryModel> RequestMachineries { get; set; } = new();
}

public record GetDailyProjectOperationByIdRequestMachineryModel
{
    public long Id { get; set; }
    public long RequestMachineryId { get; set; }
    public long? RequestNumber { get; set; }
    public RequestMachineryUnit Unit { get; set; }
    public string? UnitDescription => Unit.GetEnumDescription();
    public string FinalValue { get; set; } = string.Empty;
    public string UnusedValue { get; set; } = string.Empty;
    public decimal? Number { get; set; }
}