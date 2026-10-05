using Engineering.Domain.Entities.ProcesVerbal.Enums;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;

namespace Engineering.Domain.Entities.ProcesVerbal;

[Description(ProcesVerbalCmts.ProcesVerbalProduct)]
public class ProcesVerbalProduct : AuditableEntity<ProcesVerbalProduct, long>
{
    [Description(ProcesVerbalCmts.ProcesVerbal)]
    public ProcesVerbal ProcesVerbal { get; private set; } = null!;
    public long ProcesVerbalId { get; private set; }

    [Description(ProcesVerbalCmts.ConsumableVolumeProduct)]
    public ConsumableVolumeProduct ConsumableVolumeProduct { get; private set; } = null!;
    public long ConsumableVolumeProductId { get; private set; }

    [Description(ProcesVerbalCmts.NewFinalValue)]
    public decimal NewFinalValue { get; private set; }

    [Description(ProcesVerbalCmts.ProcesVerbalProductItemStatus)]
    public ProcesVerbalProductItemStatus ProcesVerbalProductItemStatus { get; private set; }

    [Description(ProcesVerbalCmts.Description)]
    public string? Description { get; private set; }


    public ProcesVerbalProduct(
        ProcesVerbal procesVerbal,
        ConsumableVolumeProduct consumableVolumeProduct,
        decimal newFinalValue,
        ProcesVerbalProductItemStatus status,
        string? description) : this()
    {
        SetProcesVerbal(procesVerbal);
        SetConsumableVolumeProduct(consumableVolumeProduct);
        SetNewFinalValue(newFinalValue);
        SetStatus(status);
        SetDescription(description);
    }

    public static ProcesVerbalProduct Create(
        ProcesVerbal procesVerbal,
        ConsumableVolumeProduct consumableVolumeProduct,
        decimal newFinalValue,
        ProcesVerbalProductItemStatus status,
        string? description)
            => new(procesVerbal, consumableVolumeProduct, newFinalValue, status, description);


    #region Commands

    public void SetNewFinalValue(decimal value)
    {
        NewFinalValue = value;
    }
    public void SetStatus(ProcesVerbalProductItemStatus value)
    {
        ProcesVerbalProductItemStatus = value;
    }
    public void SetDescription(string? value)
    {
        Description = value;
    }


    public void SetProcesVerbal(ProcesVerbal value)
    {
        ProcesVerbal = Guard.Against.Null(value, nameof(value));
        ProcesVerbalId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetConsumableVolumeProduct(ConsumableVolumeProduct value)
    {
        ConsumableVolumeProduct = Guard.Against.Null(value, nameof(value));
        ConsumableVolumeProductId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    #endregion

    private ProcesVerbalProduct() { }
}