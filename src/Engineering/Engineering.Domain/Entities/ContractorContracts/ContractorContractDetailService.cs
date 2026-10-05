using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Domain.Entities.ContractorContracts;

[Description(CCCmts.ContractorContractDetailService)]
public class ContractorContractDetailService : AuditableEntity<ContractorContractDetailService, long>
{

    #region Properties
    [Description(CCCmts.ContractorContractDetailId)]
    public long? ContractorContractDetailId { get; private set; }
    [Description(CCCmts.ContractorContractDetail)]
    public ContractorContractDetail ContractorContractDetail { get; private set; }
    [Description(CCCmts.ProjectOperationDetailContractorServiceId)]
    public long ProjectOperationDetailContractorServiceId { get; private set; }
    [Description(CCCmts.ProjectOperationDetailContractorService)]
    public ProjectOperationDetailContractorService ProjectOperationDetailContractorService { get; private set; }

    #endregion

    public ContractorContractDetailService(
        ContractorContractDetail contractorContractDetail,
        ProjectOperationDetailContractorService contractorService) : this()
    {
        SetContractorContractDetail(contractorContractDetail);
        SetProjectOperationDetailContractorService(contractorService);
    }


    #region Commands

    public static ContractorContractDetailService Create(
        ContractorContractDetail contractorContractDetail,
        ProjectOperationDetailContractorService contractorService)
    {
        return new ContractorContractDetailService(
            contractorContractDetail,
            contractorService);
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
        if (ProjectOperationDetailContractorService is not null)
            ProjectOperationDetailContractorService.SetStatusNew();
    }
    public void SetContractorContractDetail(ContractorContractDetail value)
    {
        ContractorContractDetail = Guard.Against.Null(value, nameof(value));
        ContractorContractDetailId = Guard.Against.Null(value.Id, nameof(value.Id));
    }
    public void SetProjectOperationDetailContractorService(ProjectOperationDetailContractorService value)
    {
        ProjectOperationDetailContractorService = Guard.Against.Null(value, nameof(value));
        ProjectOperationDetailContractorServiceId = Guard.Against.Null(value.Id, nameof(value.Id));
        ProjectOperationDetailContractorService.SetStatusContract();
    }

    #endregion

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private ContractorContractDetailService()
    {

    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

}
