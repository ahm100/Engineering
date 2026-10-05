using Engineering.Domain.Entities.Contracts;
using Engineering.Domain.Entities.ProcesVerbal.Enums;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Domain.Entities.ProcesVerbal;

[Description(ProcesVerbalCmts.ProcesVerbal)]
public class ProcesVerbal : ActivateEntity<ProcesVerbal, long>
{
    [Description(ProcesVerbalCmts.TitleFa)]
    public string TitleFa { get; private set; } = string.Empty;

    [Description(ProcesVerbalCmts.TitleEn)]
    public string? TitleEn { get; private set; } = string.Empty;

    [Description(ProcesVerbalCmts.Type)]
    public ProcesVerbalType Type { get; private set; }

    [Description(ProcesVerbalCmts.RecordDateTime)]
    public DateTime RecordDateTime { get; private set; }

    [Description(ProcesVerbalCmts.Location)]
    public string? Location { get; private set; }

    [Description(ContractCmts.Contract)]
    public Contract Contract { get; private set; }
    public long ContractId { get; private set; }

    [Description(ProjectCmts.Project)]
    public Project Project { get; private set; }
    public long ProjectId { get; private set; }

    [Description(ProcesVerbalCmts.DeliveryStatus)]
    public ProcesVerbalDeliveryStatus? DeliveryStatus { get; private set; }

    [Description(ProcesVerbalCmts.Limitations)]
    public string? Limitations { get; private set; }

    [Description(ProcesVerbalCmts.ProductStatus)]
    public ProcesVerbalProductStatus? ProductStatus { get; private set; }

    [Description(ProcesVerbalCmts.WorkStatus)]
    public ProcesVerbalWorkStatus? WorkStatus { get; private set; }

    [Description(ProcesVerbalCmts.LimitationStatus)]
    public ProcesVerbalLimitationStatus? LimitationStatus { get; private set; }

    [Description(ProcesVerbalCmts.WorkStartStatus)]
    public ProcesVerbalWorkStartStatus? WorkStartStatus { get; private set; }

    [Description(ProcesVerbalCmts.WorkStopReason)]
    public ProcesVerbalWorkStopReason? WorkStopReason { get; private set; }

    [Description(ProcesVerbalCmts.WorkStopStatus)]
    public ProcesVerbalWorkStopStatus? WorkStopStatus { get; private set; }

    public ProcesVerbal(
        string titleFa,
        string? titleEn,
        ProcesVerbalType type,
        DateTime recordDateTime,
        string? location,
        long contractId,
        long projectId,
        ProcesVerbalDeliveryStatus? deliveryStatus,
        string? limitations,
        ProcesVerbalProductStatus? productStatus) : this()
    {
        SetTitleFa(titleFa);
        SetTitleEn(titleEn);
        SetType(type);
        SetRecordDateTime(recordDateTime);
        SetLocation(location);
        SetContractId(contractId);
        SetProjectId(projectId);
        SetDeliveryStatus(deliveryStatus);
        SetLimitations(limitations);
        SetProductStatus(productStatus);
    }

    public static ProcesVerbal Create(
        string titleFa,
        string? titleEn,
        ProcesVerbalType type,
        DateTime recordDateTime,
        string? location,
        long contractId,
        long projectId,
        ProcesVerbalDeliveryStatus? deliveryStatus,
        string? limitations,
        ProcesVerbalProductStatus? productStatus)
            => new(titleFa, titleEn, type, recordDateTime, location, contractId, projectId,
                deliveryStatus, limitations, productStatus);
    
    public string GenerateCode(string? ProjectCode, long Year) => $"{ProjectCode}-{Year.ToString()}-{Id.ToString()}";

    #region Set data

    public void SetTitleFa(string value)
    {
        TitleFa = Guard.Against.Null(value, nameof(value));
    }
    public void SetTitleEn(string? value)
    {
        TitleEn = value;
    }
    public void SetType(ProcesVerbalType value)
    {
        Type = value;
    }
    public void SetRecordDateTime(DateTime value)
    {
        RecordDateTime = value;
    }
    public void SetLocation(string? value)
    {
        Location = value;
    }
    public void SetLimitations(string? value)
    {
        Limitations = value;
    }
    public void SetDeliveryStatus(ProcesVerbalDeliveryStatus? value)
    {
        DeliveryStatus = value;
    }
    public void SetProductStatus(ProcesVerbalProductStatus? value)
    {
        ProductStatus = value;
    }
    public void SetWorkStatus(ProcesVerbalWorkStatus? value)
    {
        WorkStatus = value;
    }
    public void SetLimitationStatus(ProcesVerbalLimitationStatus? value)
    {
        LimitationStatus = value;
    }
    public void SetWorkStartStatus(ProcesVerbalWorkStartStatus? value)
    {
        WorkStartStatus = value;
    }
    public void SetWorkStopReason(ProcesVerbalWorkStopReason? value)
    {
        WorkStopReason = value;
    }
    public void SetWorkStopStatus(ProcesVerbalWorkStopStatus? value)
    {
        WorkStopStatus = value;
    }

    public void SetContract(Contract value)
    {
        Contract = value;
        ContractId = value.Id;
    }
    public void SetContractId(long value)
    {
        ContractId = value;
    }


    public void SetProject(Project value)
    {
        Project = value;
        ProjectId = value.Id;
    }
    public void SetProjectId(long value)
    {
        ProjectId = value;
    }

    #endregion

    #region Methods

    public void AddDocument(ProcesVerbalDoc newData)
    {
        ArgumentNullException.ThrowIfNull(newData);
        _procesVerbalDocuments.Add(newData);
    }

    public void AddItem(ProcesVerbalItem newData)
    {
        ArgumentNullException.ThrowIfNull(newData);
        _procesVerbalItems.Add(newData);
    }

    public void AddPOD(ProcesVerbalPOD newData)
    {
        ArgumentNullException.ThrowIfNull(newData);
        _procesVerbalPODs.Add(newData);
    }

    public void AddProduct(ProcesVerbalProduct newData)
    {
        ArgumentNullException.ThrowIfNull(newData);
        _procesVerbalProducts.Add(newData);
    }

    #endregion

#pragma warning disable CS8618
    [Description(ProcesVerbalCmts.ProcesVerbalDoc)]
    private List<ProcesVerbalDoc> _procesVerbalDocuments;
    public IReadOnlyList<ProcesVerbalDoc> ProcesVerbalDocuments => _procesVerbalDocuments;

    [Description(ProcesVerbalCmts.ProcesVerbalItem)]
    private List<ProcesVerbalItem> _procesVerbalItems;
    public IReadOnlyList<ProcesVerbalItem> ProcesVerbalItems => _procesVerbalItems;

    [Description(ProcesVerbalCmts.ProcesVerbalPOD)]
    private List<ProcesVerbalPOD> _procesVerbalPODs;
    public IReadOnlyList<ProcesVerbalPOD> ProcesVerbalPODs => _procesVerbalPODs;

    [Description(ProcesVerbalCmts.ProcesVerbalProduct)]
    private List<ProcesVerbalProduct> _procesVerbalProducts;
    public IReadOnlyList<ProcesVerbalProduct> ProcesVerbalProducts => _procesVerbalProducts;

    private ProcesVerbal()
    {
        _procesVerbalDocuments = [];
        _procesVerbalItems = [];
        _procesVerbalPODs = [];
        _procesVerbalProducts = [];
    }
#pragma warning restore CS8618
}