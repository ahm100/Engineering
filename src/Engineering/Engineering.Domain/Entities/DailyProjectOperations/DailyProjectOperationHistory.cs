using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Domain.Entities.DailyProjectOperations;

[Description(DailyProjectOperationCmts.DailyProjectOperationHistory)]
public class DailyProjectOperationHistory : AuditableEntity<DailyProjectOperationHistory, long>
{
    #region Properties

    [Description(DailyProjectOperationCmts.LegacyId)]
    public long? LegacyId { get; private set; }

    [Description(DailyProjectOperationCmts.ProjectOperationDetailStatus)]
    public ProjectOperationDetailStatus Status { get; private set; }

    [Description(DailyProjectOperationCmts.Type)]
    public DailyProjectOperationType Type { get; private set; }

    [Description(GlobalCmts.StartDate)]
    public DateTime StartDate { get; private set; }

    [Description(GlobalCmts.EndDate)]
    public DateTime EndDate { get; private set; }

    [Description(DailyProjectOperationCmts.Length)]
    public decimal Length { get; private set; } = 0;

    [Description(DailyProjectOperationCmts.Width)]
    public decimal Width { get; private set; } = 0;

    [Description(DailyProjectOperationCmts.Height)]
    public decimal Height { get; private set; } = 0;

    [Description(DailyProjectOperationCmts.Weight)]
    public decimal Weight { get; private set; } = 0;

    [Description(DailyProjectOperationCmts.Number)]
    public decimal Number { get; private set; } = 0;

    [Description(DailyProjectOperationCmts.FinalAmount)]
    public decimal FinalAmount => Length * Width * Height * Weight * Number;

    [Description(GlobalCmts.CompanyId)]
    public long? CompanyId { get; private set; }

    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; } = string.Empty;

    [Description(DailyProjectOperationCmts.DailyProjectOperation)]
    public DailyProjectOperation DailyProjectOperation { get; private set; }
    public long DailyProjectOperationId { get; private set; }


    public DailyProjectOperationHistory(
        DailyProjectOperation daily) : this()
    {
        SetDailyProjectOperation(daily);
        SetStatus(daily.Status);
        SetStartDate(daily.StartDate);
        SetEndDate(daily.EndDate);
        SetLength(daily.Length);
        SetWidth(daily.Width);
        SetHeight(daily.Height);
        SetWeight(daily.Weight);
        SetNumber(daily.Number);
        SetCompanyId(daily.CompanyId);
        SetLegacyId(daily.LegacyId);
        SetDescription(daily.Description);
        SetType(daily.Type);
    }

    #endregion

    #region SetData

    public void SetDailyProjectOperation(DailyProjectOperation value)
    {
        DailyProjectOperation = Guard.Against.Null(value, nameof(value));
        DailyProjectOperationId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetStatus(ProjectOperationDetailStatus value)
    {
        Status = Guard.Against.Null(value, nameof(value));
    }

    public void SetType(DailyProjectOperationType value)
    {
        Type = Guard.Against.Null(value, nameof(value));
    }

    public void SetStartDate(DateTime value)
    {
        StartDate = Guard.Against.Null(value, nameof(value));
    }

    public void SetEndDate(DateTime value)
    {
        EndDate = Guard.Against.Null(value, nameof(value));
    }

    public void SetLength(decimal value)
    {
        Length = Guard.Against.Null(value, nameof(value));
    }

    public void SetWidth(decimal value)
    {
        Width = Guard.Against.Null(value, nameof(value));
    }

    public void SetWeight(decimal value)
    {
        Weight = Guard.Against.Null(value, nameof(value));
    }

    public void SetNumber(decimal value)
    {
        Number = Guard.Against.Null(value, nameof(value));
    }

    public void SetHeight(decimal value)
    {
        Height = Guard.Against.Null(value, nameof(value));
    }

    public void SetCompanyId(long? value)
    {
        CompanyId = value;
    }

    public void SetLegacyId(long? value)
    {
        LegacyId = value;
    }

    public void SetDescription(string? value)
    {
        Description = value;
    }

    #endregion

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private DailyProjectOperationHistory()
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
