using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.DataModels;

public record GetsByMachineryIdModel(
    long Id,
    long ProjectOperationId,
    long ProjectId,
    string? ProjectName,
    long OperationInfoId,
    string? OperationInfoName,
    string? StartDate,
    string? EndDate,
    decimal Length,
    bool LengthChangeable,
    decimal Width,
    bool WidthChangeable,
    decimal Height,
    bool HeightChangeable,
    decimal Weight,
    bool WeightChangeable,
    decimal Number,
    bool NumberChangeable,
    decimal FinalAmount,
    ProjectOperationDetailStatus Status,
    string? StatusTitle,
    int Priority,
    int Day,
    int Hour,
    string? Description
    );


