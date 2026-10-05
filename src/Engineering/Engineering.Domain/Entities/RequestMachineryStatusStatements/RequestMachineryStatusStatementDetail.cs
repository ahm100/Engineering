using Engineering.Domain.Entities.Machineries;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.RequestMachineryStatusStatements.Enums;

namespace Engineering.Domain.Entities.RequestMachineryStatusStatements;

[Description(RequestMachineryStatusStatementCmts.RequestMachineryStatusStatementDetail)]
public class RequestMachineryStatusStatementDetail : AuditableEntity<RequestMachineryStatusStatementDetail>
{
    #region Properties

    [Description(RequestMachineryStatusStatementCmts.ContractorId)]
    public long ContractorId { get; private set; }

    [Description(RequestMachineryStatusStatementCmts.FromDate)]
    public DateTime? FromDate { get; private set; }

    [Description(RequestMachineryStatusStatementCmts.ToDate)]
    public DateTime? ToDate { get; private set; }

    [Description(RequestMachineryStatusStatementCmts.RequestedCount)]
    public decimal RequestedCount { get; private set; }

    [Description(RequestMachineryStatusStatementCmts.FinalPrice)]
    public decimal FinalPrice { get; private set; }

    [Description(RequestMachineryStatusStatementCmts.CurrencyId)]
    public long? CurrencyId { get; private set; }

    [Description(RequestMachineryStatusStatementCmts.Unit)]
    public RequestMachineryStatusStatementUnit Unit { get; private set; }

    [Description(RequestMachineryStatusStatementCmts.OperatorId)]
    public long? OperatorId { get; private set; }

    [Description(RequestMachineryStatusStatementCmts.TimeRequired)]
    public decimal? TimeRequired { get; private set; }

    [Description(RequestMachineryStatusStatementCmts.Project)]
    public long ProjectId { get; private set; }
    public Project Project { get; private set; }

    [Description(RequestMachineryStatusStatementCmts.Machinery)]
    public long MachineryId { get; private set; }
    public Machinery Machinery { get; private set; }

    [Description(RequestMachineryStatusStatementCmts.RequestMachinery)]
    public long RequestMachineryId { get; private set; }
    public RequestMachinery RequestMachinery { get; private set; }

    [Description(RequestMachineryStatusStatementCmts.RequestMachineryStatusStatement)]
    public long RequestMachineryStatusStatementId { get; set; }
    public RequestMachineryStatusStatement RequestMachineryStatusStatement { get; set; }

    #endregion

    public RequestMachineryStatusStatementDetail(
     Project project,
     Machinery machinery,
     RequestMachinery requestMachinery,
     RequestMachineryStatusStatement requestMachineryStatusStatement,
     long contractorId,
     DateTime? fromDate,
     DateTime? toDate,
     decimal requestedCount,
     decimal finalPrice,
     long? currencyId,
     RequestMachineryStatusStatementUnit unit,
     long? operatorId,
     decimal? timeRequired) : this()
    {
        SetProject(project);
        SetMachinery(machinery);
        SetRequestMachinery(requestMachinery);
        SetRequestMachineryStatusStatement(requestMachineryStatusStatement);
        SetContractorId(contractorId);
        SetRequestedCount(requestedCount);
        SetFinalPrice(finalPrice);
        SetUnit(unit);
        SetCurrencyId(currencyId);
        SetFromDate(fromDate);
        SetToDate(toDate);
        SetTimeRequired(timeRequired);
        SetOperatorId(operatorId);
    }

    public void SetData(
     Project project,
     Machinery machinery,
     RequestMachinery requestMachinery,
     RequestMachineryStatusStatement requestMachineryStatusStatement,
     long contractorId,
     DateTime? fromDate,
     DateTime? toDate,
     decimal requestedCount,
     decimal finalPrice,
     long? currencyId,
     RequestMachineryStatusStatementUnit unit,
     long? operatorId,
     decimal? timeRequired)
    {
        SetProject(project);
        SetMachinery(machinery);
        SetRequestMachinery(requestMachinery);
        SetRequestMachineryStatusStatement(requestMachineryStatusStatement);
        SetContractorId(contractorId);
        SetRequestedCount(requestedCount);
        SetFinalPrice(finalPrice);
        SetUnit(unit);
        SetCurrencyId(currencyId);
        SetFromDate(fromDate);
        SetToDate(toDate);
        SetTimeRequired(timeRequired);
        SetOperatorId(operatorId);
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetContractorId(long value)
    {
        ContractorId = Guard.Against.Null(value, nameof(value));
    }

    public void SetOperatorId(long? value)
    {
        OperatorId = value;
    }

    public void SetCurrencyId(long? value)
    {
        OperatorId = value;
    }

    public void SetUnit(RequestMachineryStatusStatementUnit value)
    {
        Unit = Guard.Against.Null(value, nameof(value));
    }

    public void SetRequestedCount(decimal value)
    {
        RequestedCount = Guard.Against.Null(value, nameof(value));
    }

    public void SetFinalPrice(decimal value)
    {
        FinalPrice = Guard.Against.Null(value, nameof(value));
    }

    public void SetFromDate(DateTime? value)
    {
        FromDate = value;
    }

    public void SetToDate(DateTime? value)
    {
        ToDate = value;
    }

    public void SetMachinery(Machinery value)
    {
        Machinery = Guard.Against.Null(value, nameof(value));
        MachineryId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetTimeRequired(decimal? value)
    {
        TimeRequired = value;
    }

    public void SetProject(Project value)
    {
        Project = Guard.Against.Null(value, nameof(value));
    }

    public void SetRequestMachinery(RequestMachinery value)
    {
        RequestMachinery = Guard.Against.Null(value, nameof(value));
    }

    public void SetRequestMachineryStatusStatement(RequestMachineryStatusStatement value)
    {
        RequestMachineryStatusStatement = Guard.Against.Null(value, nameof(value));
    }

    public void AddProjectOepration(ProjectOperation value)
    {
        var item = new RequestMachineryStatusStatementDetailProjectOperation(this, value);
        _requestMachineryStatusStatementDetailProjectOperations.Add(item);
    }

    public void AddProjectOeprationDetail(ProjectOperationDetail value)
    {
        var item = new RequestMachineryStatusStatementDetailProjectOperationDetail(this, value);
        _requestMachineryStatusStatementDetailProjectOperationDetails.Add(item);
    }
    #region Constructors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private readonly List<RequestMachineryStatusStatementDetailProjectOperation> _requestMachineryStatusStatementDetailProjectOperations;
    public IReadOnlyList<RequestMachineryStatusStatementDetailProjectOperation> StatusStatementDetailProjectOperations => _requestMachineryStatusStatementDetailProjectOperations;

    private readonly List<RequestMachineryStatusStatementDetailProjectOperationDetail> _requestMachineryStatusStatementDetailProjectOperationDetails;
    public IReadOnlyList<RequestMachineryStatusStatementDetailProjectOperationDetail> StatusStatementDetailProjectOperationDetails => _requestMachineryStatusStatementDetailProjectOperationDetails;

    private RequestMachineryStatusStatementDetail()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        _requestMachineryStatusStatementDetailProjectOperationDetails = [];
        _requestMachineryStatusStatementDetailProjectOperations = [];
    }


    #endregion

    #region Commands

    #endregion
}
