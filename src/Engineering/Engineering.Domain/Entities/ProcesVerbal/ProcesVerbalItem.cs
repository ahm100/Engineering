namespace Engineering.Domain.Entities.ProcesVerbal;

[Description(ProcesVerbalCmts.ProcesVerbalItem)]
public class ProcesVerbalItem : AuditableEntity<ProcesVerbalItem, long>
{
    [Description(ProcesVerbalCmts.ProcesVerbal)]
    public ProcesVerbal ProcesVerbal { get; private set; } = null!;
    public long ProcesVerbalId { get; private set; }

    [Description(ProcesVerbalCmts.TitleFa)]
    public string TitleFa { get; private set; } = string.Empty;

    public ProcesVerbalItem(
        ProcesVerbal procesVerbal,
        string titleFa) : this()
    {
        SetProcesVerbal(procesVerbal);
        SetTitleFa(titleFa);
    }

    public static ProcesVerbalItem Create(ProcesVerbal procesVerbal, string titleFa)
        => new(procesVerbal, titleFa);

    #region Commands

    public void SetData(ProcesVerbal procesVerbal, string titleFa)
    {
        SetProcesVerbal(procesVerbal);
        SetTitleFa(titleFa);
    }

    public void SetTitleFa(string value) => TitleFa = Guard.Against.Null(value, nameof(value));

    public void SetProcesVerbal(ProcesVerbal value)
    {
        ProcesVerbal = Guard.Against.Null(value, nameof(value));
        ProcesVerbalId = Guard.Against.Null(value.Id, nameof(value.Id));
    }
    #endregion

    private ProcesVerbalItem() { }
}