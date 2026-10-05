using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Domain.Entities.ProcesVerbal;

[Description(ProcesVerbalCmts.ProcesVerbalPOD)]
public class ProcesVerbalPOD : AuditableEntity<ProcesVerbalPOD, long>
{
    [Description(ProcesVerbalCmts.ProcesVerbal)]
    public ProcesVerbal ProcesVerbal { get; private set; } = null!;
    public long ProcesVerbalId { get; private set; }

    [Description(ProcesVerbalCmts.ProjectOperationDetail)]
    public ProjectOperationDetail ProjectOperationDetail { get; private set; } = null!;
    public long ProjectOperationDetailId { get; private set; }

    [Description(ProcesVerbalCmts.NewFinalAmout)]
    public decimal NewFinalAmount { get; private set; }

    public ProcesVerbalPOD(ProcesVerbal pv, ProjectOperationDetail pod, decimal newFinalAmount) : this()
    {
        SetProcesVerbal(pv);
        SetProjectOperationDetail(pod);
        SetNewFinalAmount(newFinalAmount);
    }


    public static ProcesVerbalPOD Create(ProcesVerbal procesVerbal,
        ProjectOperationDetail projectOperationDetail,
        decimal newFinalAmount)
        => new(procesVerbal, projectOperationDetail, newFinalAmount);

    #region Commands
    public void SetNewFinalAmount(decimal value)
    {
        NewFinalAmount = value;
    }

    public void SetProcesVerbal(ProcesVerbal value)
    {
        ProcesVerbal = Guard.Against.Null(value, nameof(value));
        ProcesVerbalId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetProjectOperationDetail(ProjectOperationDetail value)
    {
        ProjectOperationDetail = Guard.Against.Null(value, nameof(value));
        ProjectOperationDetailId = Guard.Against.Null(value.Id, nameof(value.Id));
    }
    #endregion

    private ProcesVerbalPOD() { }
}