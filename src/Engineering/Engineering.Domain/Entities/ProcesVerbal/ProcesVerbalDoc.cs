namespace Engineering.Domain.Entities.ProcesVerbal;

[Description(ProcesVerbalCmts.ProcesVerbalDoc)]
public class ProcesVerbalDoc : AuditableEntity<ProcesVerbalDoc, long>
{
    [Description(GlobalCmts.Url)]
    public string URL { get; private set; } = string.Empty;


    [Description(ProcesVerbalCmts.ProcesVerbal)]
    public long ProcesVerbalId { get; private set; }
    public ProcesVerbal ProcesVerbal { get; private set; } = null!;

    public ProcesVerbalDoc(string url,
        ProcesVerbal procesVerbal) : this()
    {
        SetURL(url);
        SetProcesVerbal(procesVerbal);
    }

    #region Commands

    public void SetData(string url,
        ProcesVerbal procesVerbal)
    {
        SetURL(url);
        SetProcesVerbal(procesVerbal);
    }

    public void SetURL(string value)
    {
        URL = Guard.Against.Null(value, nameof(value));
    }

    public void SetProcesVerbal(ProcesVerbal value)
    {
        ProcesVerbal = Guard.Against.Null(value, nameof(value));
        ProcesVerbalId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    #endregion

    private ProcesVerbalDoc() { }
}
