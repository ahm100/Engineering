namespace Engineering.Application.Services.ProjectOperationDetailDeductions.Models.GetProjectOperationDetailDeductionById;

public record GetProjectOperationDetailDeductionByIdResponse(
    long Id,
    long ProjectOperationDetailId,
    string? PrivateName,
    string? PrivateCode,
    string? PublicName,
    string? PublicCode,
    decimal Length,
    decimal Width,
    decimal Height,
    decimal Weight,
    decimal Number,
    decimal FinalAmount
    );

