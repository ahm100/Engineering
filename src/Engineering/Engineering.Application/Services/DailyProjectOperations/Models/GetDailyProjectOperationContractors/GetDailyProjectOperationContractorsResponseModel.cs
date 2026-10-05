namespace Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationContractors;

public record GetDailyProjectOperationContractorsResponseModel
{
    public long? Id { get; set; }
    public long? UserId { get; set; }
    public string? FullName { get; set; }
    public string? Nickname { get; set; }
};
