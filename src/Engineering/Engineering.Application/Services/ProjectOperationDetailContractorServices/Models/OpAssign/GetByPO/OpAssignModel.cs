namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.OpAssign.GetByPO;

public class OpAssignModel
{
    public long Id { get; set; }

    public long ProjectOperationDetailId { get; set; }
    public string? ProjectOperationDetailCode { get; set; }
    public string? ProjectOperationDetailDescription { get; set; }

    public long? ContractorId { get; set; }
    public string? ContractorName { get; set; }
    public string? ContractorOrganizationCode { get; set; }

    public decimal Volume { get; set; }

    public bool HasContract { get; set; }
    public bool CanEdit { get; set; }
    public bool CanDelete { get; set; }
}