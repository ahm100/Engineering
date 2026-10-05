using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;

namespace Engineering.Application.Services.ProjectOperationDetailContractorExperts.Commands.CreateBatchPODContractorExpert;

public record CreateBatchPODContractorExpertCommand(
    List<CreateBatchPODContractorExpertModel> Models) : ICommand<bool?>;

public class CreateBatchPODContractorExpertModel
{
    public required ConsumableVolumeExpert ConsumableVolumeExpert { get; set; }
    public required ProjectOperationDetailContractorService ProjectOperationDetailContractorService { get; set; }
    public decimal Volume { get; set; }
    public bool IsActive { get; set; }
}