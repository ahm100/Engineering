namespace Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentReferences;

public class GetContractAdjustmentReferencesModel
{
    public long Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public string FaTitle { get; set; } = string.Empty;

    public string EnTitle { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsActive { get; set; }
}
