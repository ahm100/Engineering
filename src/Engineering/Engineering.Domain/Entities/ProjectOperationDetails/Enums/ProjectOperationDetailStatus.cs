
namespace Engineering.Domain.Entities.ProjectOperationDetails.Enums;

public enum ProjectOperationDetailStatus
{
    /// <summary>
    /// شروع نشده
    /// </summary>
    [Description("شروع نشده")]
    NotStarted = 1,
    /// <summary>
    /// درحال انجام
    /// </summary>
    [Description("درحال انجام")]
    Doing = 2,
    /// <summary>
    /// متوقف شده
    /// </summary>
    [Description("متوقف شده")]
    Stopped = 3,
    /// <summary>
    /// پایان کار
    /// </summary>
    [Description("پایان کار")]
    EndOfWork = 4,
    /// <summary>
    /// تحویل موقت
    /// </summary>
    [Description("تحویل موقت")]
    TemporaryDelivery = 5,
    /// <summary>
    /// تحویل قطعی
    /// </summary>
    [Description("تحویل قطعی")]
    DefiniteDelivery = 6,
}

public class ProjectOperationDetailStatusExtensions
{
    public static readonly Dictionary<ProjectOperationDetailStatus, int> ProjectOperationDetailStatusOrder =
       new Dictionary<ProjectOperationDetailStatus, int>() {
      {ProjectOperationDetailStatus.Doing, 1},
      {ProjectOperationDetailStatus.TemporaryDelivery, 2},
      {ProjectOperationDetailStatus.DefiniteDelivery, 3},
      {ProjectOperationDetailStatus.EndOfWork, 4},
      {ProjectOperationDetailStatus.Stopped, 5},
      {ProjectOperationDetailStatus.NotStarted, 6},
};
}

