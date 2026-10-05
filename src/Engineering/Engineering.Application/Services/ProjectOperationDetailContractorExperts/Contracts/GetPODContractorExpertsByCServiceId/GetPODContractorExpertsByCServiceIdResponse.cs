using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ProjectOperationDetailContractorExperts.Contracts.GetPODContractorExpertsByCServiceId;

public record GetPODContractorExpertsByCServiceIdResponse(
    List<GetPODContractorExpertsByCServiceIdModel> Data,
    int RowCount);

public class GetPODContractorExpertsByCServiceIdModel
{
    public long Id { get; set; }
    public decimal Volume { get; set; }
    public bool HaveContract { get; set; }
    public long ConsumableVolumeExpertId { get; set; }
    public long SkillId { get; set; }
    public string? Skill { get; set; }
    public long ProjectOperationDetailContractorServiceId { get; set; }
    public long? ContractorId { get; set; }
    public string? Contractor { get; set; }
    public ContractorServiceStatus ContractorServiceStatus { get; set; }
    public string ContractorServiceStatusDescription => ContractorServiceStatus.GetEnumDescription();
    public DateTime Created { get; set; }
    public string CreatedShamsi => Created.ToShamsi();
    public long CreatorId { get; set; }
    public string? Creator { get; set; }
    public DateTime? Updated { get; set; }
    public string? UpdatedShamsi => Updated.ToShamsi();
    public long? UpdatorId { get; set; }
    public string? Updator { get; set; }
}