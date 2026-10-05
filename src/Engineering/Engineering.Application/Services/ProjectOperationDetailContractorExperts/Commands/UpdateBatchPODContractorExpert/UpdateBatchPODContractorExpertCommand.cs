namespace Engineering.Application.Services.ProjectOperationDetailContractorExperts.Commands.UpdateBatchPODContractorExpert;

public record UpdateBatchPODContractorExpertCommand(
    List<UpdateBatchPODContractorExpertModel> Models) : ICommand<bool?>;

public class UpdateBatchPODContractorExpertModel
{
    public long Id { get; set; }
    public decimal Volume { get; set; }
    public bool IsActive { get; set; }
}