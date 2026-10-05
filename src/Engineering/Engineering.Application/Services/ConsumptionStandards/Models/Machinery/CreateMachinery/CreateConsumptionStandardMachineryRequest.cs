namespace Engineering.Application.Services.ConsumptionStandards.Models.Machinery.CreateMachinery;

public record CreateConsumptionStandardMachineryRequest(
    long Id,
    int MachineryNumber,
    string TimeSpant,
    decimal? UnusedPercentage,
    long OperationInfoId
     ) : IHttpRequest;
