using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.RequestMachineries.Models.GetRequestMachineryHistories;

public record GetRequestMachineryHistoriesModel
{
    public RequestMachineryStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public DateTime Created { get; set; }
    public long? CreatorId { get; set; }
    public string? Creator { get; set; }
    public string? Operator { get; set; }
    public string? Description { get; set; } = string.Empty;
    public string? RequestDescription { get; set; } = string.Empty;
    public long? MachineryId { get; set; }
    public string? Machinery { get; set; }
    public DateTime? ConfirmFromDate { get; set; }
    public TimeSpan? ConfirmFromTime => ConfirmFromDate?.TimeOfDay;
    public DateTime? ConfirmToDate { get; set; }
    public TimeSpan? ConfirmToTime => ConfirmToDate?.TimeOfDay;
    public string? ConfirmDescription { get; set; }
    public string? ManagerDescription { get; set; }
    public decimal? ConfirmTimeRequired { get; set; }
    public long? ContractorId { get; set; }
    public string? Contractor { get; set; }
}
