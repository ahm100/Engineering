namespace Engineering.Application.Services.Machineries.Models.CreateMachinery;

public record CreateMachineryResponse(
    long Id,
    string MachineryCode,
    string MachineryName,
    long GroupId,
    string GroupName,
    string GroupCode,
    bool IsActive
    );
