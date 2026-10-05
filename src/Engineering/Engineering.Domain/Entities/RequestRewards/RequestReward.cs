using Engineering.Domain.Entities.ContractorStatusStatements;
using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.DailyProjectOperations;
using Engineering.Domain.Entities.FiduciaryProducts;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.RequestRewards.Enums;

namespace Engineering.Domain.Entities.RequestRewards;

[Description(GlobalCmts.RequestReward)]
public class RequestReward : AuditableEntity<RequestReward>
{
    #region Properties

    [Description(RequestRewardCmts.OfferedPrice)]
    public decimal? OfferedPrice { get; private set; } = 0;

    [Description(RequestRewardCmts.ConfirmedPrice)]
    public decimal ConfirmedPrice { get; private set; }

    [Description(GlobalCmts.Description)]
    public string Description { get; private set; } = string.Empty;

    [Description(RequestRewardCmts.RegistrationDate)]
    public DateTime RegistrationDate { get; private set; }

    [Description(RequestRewardCmts.Type)]
    public RequestRewardType Type { get; private set; }

    [Description(GlobalCmts.Status)]
    public RequestRewardStatus Status { get; private set; } = RequestRewardStatus.New;

    [Description(GlobalCmts.CurrencyId)]
    public long? CurrencyId { get; private set; }

    [Description(RequestRewardCmts.ManagerDescription)]
    public string? ManagerDescription { get; private set; } = string.Empty;

    [Description(GlobalCmts.CompanyId)]
    public long? CompanyId { get; private set; }

    [Description(GlobalCmts.CostCenter)]
    public long? CostCenterId { get; private set; }
    public CostCenter? CostCenter { get; private set; }

    [Description(GlobalCmts.Project)]
    public long? ProjectId { get; private set; }
    public Project? Project { get; private set; }

    [Description(GlobalCmts.ProjectOperation)]
    public long? ProjectOperationId { get; private set; }
    public ProjectOperation? ProjectOperation { get; private set; }

    [Description(GlobalCmts.ProjectOperationDetail)]
    public long? ProjectOperationDetailId { get; private set; }
    public ProjectOperationDetail? ProjectOperationDetail { get; private set; }

    [Description(FiduciaryProductCmts.FiduciaryProductDetailReturn)]
    public long? FiduciaryProductDetailReturnId { get; private set; }
    public FiduciaryProductDetailReturn? FiduciaryProductDetailReturn { get; private set; }

    #endregion

    public RequestReward(decimal? offeredPrice,
        string description,
        DateTime registrationDate,
        RequestRewardType type,
        long? currencyId,
        CostCenter? costCenter,
        Project? project,
        ProjectOperation? projectOperation,
        ProjectOperationDetail? projectOperationDetail,
        FiduciaryProductDetailReturn? fiduciaryProductDetailReturn,
        long? companyId) : this()
    {
        SetOfferedPrice(offeredPrice);
        SetDescription(description);
        SetRegistrationDate(registrationDate);
        SetType(type);
        SetCurrencyId(currencyId);
        SetCostCenter(costCenter);
        SetProject(project);
        SetProjectOperation(projectOperation);
        SetProjectOperationDetail(projectOperationDetail);
        SetFiduciaryProductDetailReturn(fiduciaryProductDetailReturn);
        SetCompanyId(companyId);

        AddHistory();
    }

    #region Commands 

    public void AddHistory()
    {
        _requestRewardHistories.Add(new RequestRewardHistory(OfferedPrice,
            ConfirmedPrice,
            Status,
            Description,
            RegistrationDate,
            CurrencyId,
            ManagerDescription,
            this));
    }

    public void AddProduct(RequestRewardProduct product)
    {
        ArgumentNullException.ThrowIfNull(product);

        _requestRewardProducts.Add(product);
    }

    public void SetData(decimal? offeredPrice,
        string description,
        DateTime registrationDate,
        RequestRewardType type,
        long? currencyId,
        CostCenter? costCenter,
        Project? project,
        ProjectOperation? projectOperation,
        ProjectOperationDetail? projectOperationDetail,
        FiduciaryProductDetailReturn? fiduciaryProductDetailReturn,
        long? companyId)
    {
        SetOfferedPrice(offeredPrice);
        SetDescription(description);
        SetRegistrationDate(registrationDate);
        SetType(type);
        SetCurrencyId(currencyId);
        SetCostCenter(costCenter);
        SetProject(project);
        SetProjectOperation(projectOperation);
        SetProjectOperationDetail(projectOperationDetail);
        SetFiduciaryProductDetailReturn(fiduciaryProductDetailReturn);
        SetCompanyId(companyId);

        AddHistory();
    }

    public void SetConfirmedPrice(decimal value)
    {
        ConfirmedPrice = Guard.Against.NegativeOrZero(value, nameof(value));
    }

    public void SetCostCenter(CostCenter? value)
    {
        CostCenter = value;
        CostCenterId = value?.Id;
    }

    public void SetProject(Project? value)
    {
        Project = value;
        ProjectId = value?.Id;
    }

    public void SetProjectOperation(ProjectOperation? value)
    {
        ProjectOperation = value;
        ProjectOperationId = value?.Id;
    }

    public void SetProjectOperationDetail(ProjectOperationDetail? value)
    {
        ProjectOperationDetail = value;
        ProjectOperationDetailId = value?.Id;
    }

    public void SetFiduciaryProductDetailReturn(FiduciaryProductDetailReturn? value)
    {
        FiduciaryProductDetailReturn = value;
        FiduciaryProductDetailReturnId = value?.Id;
    }

    public void SetOfferedPrice(decimal? value)
    {
        OfferedPrice = value;
    }

    public void SetDescription(string description)
    {
        Description = Guard.Against.NullOrWhiteSpace(
            description,
            nameof(description));
    }

    public void SetRegistrationDate(DateTime registrationDate)
    {
        RegistrationDate = Guard.Against.Null(
            registrationDate,
            nameof(registrationDate));
    }

    public void SetType(RequestRewardType type)
    {
        Type = Guard.Against.EnumOutOfRange(
            type,
            nameof(type));
    }

    public void SetManagerDescription(string? value)
    {
        ManagerDescription = value;
    }

    public void SetCurrencyId(long? value)
    {
        CurrencyId = value;
    }

    public void Reject()
    {
        Status = RequestRewardStatus.Rejected;
    }

    public void Confirm()
    {
        Status = RequestRewardStatus.Confirmed;
    }

    public void Close()
    {
        Status = RequestRewardStatus.Closed;
    }

    public void Pending()
    {
        Status = RequestRewardStatus.Pending;
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetCompanyId(long? value)
    {
        CompanyId = value;
    }

    #endregion

    #region Constructors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private List<DailyProjectOperationRequestReward> _dailyProjectOperationRequestRewards;
    public IReadOnlyList<DailyProjectOperationRequestReward> DailyProjectOperationRequestRewards => _dailyProjectOperationRequestRewards;

    private List<ContractorStatusStatementFine> _contractorStatusStatementFines;
    public IReadOnlyList<ContractorStatusStatementFine> ContractorStatusStatementFines => _contractorStatusStatementFines;

    private List<ContractorStatusStatementReward> _contractorStatusStatementRewards;
    public IReadOnlyList<ContractorStatusStatementReward> ContractorStatusStatementRewards => _contractorStatusStatementRewards;

    private List<RequestRewardDocument> _requestRewardDocuments;
    public IReadOnlyList<RequestRewardDocument> RequestRewardDocuments => _requestRewardDocuments;

    private List<RequestRewardProduct> _requestRewardProducts;
    public IReadOnlyList<RequestRewardProduct> RequestRewardProducts => _requestRewardProducts;

    private List<RequestRewardThirdParty> _requestRewardThirdParties;
    public IReadOnlyList<RequestRewardThirdParty> RequestRewardThirdParties => _requestRewardThirdParties;

    private List<RequestRewardHistory> _requestRewardHistories;
    public IReadOnlyList<RequestRewardHistory> RequestRewardHistories => _requestRewardHistories;

    private List<ContractorStatusStatementDiscount> _contractorStatusStatementDiscounts;
    public IReadOnlyList<ContractorStatusStatementDiscount> ContractorStatusStatementDiscounts => _contractorStatusStatementDiscounts;

    private RequestReward()
    {
        _requestRewardDocuments = [];
        _requestRewardHistories = [];
        _requestRewardThirdParties = [];
        _requestRewardProducts = [];
        _dailyProjectOperationRequestRewards = [];
        _contractorStatusStatementFines = [];
        _contractorStatusStatementRewards = [];
        _contractorStatusStatementDiscounts = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


    #endregion
}
