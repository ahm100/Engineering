namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.OpAssign.GetAssignable;

public class AssignablePODModel
{
    public long ProjectOperationDetailId { get; set; }

    public string? Code { get; set; }
    public string? Description { get; set; }

    public decimal FinalAmount { get; set; }
    public decimal DeductionVolume { get; set; }
    public decimal AssignableVolume { get; set; }
    public decimal AssignedVolume { get; set; }
    public decimal RemainingVolume { get; set; }
}