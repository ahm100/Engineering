using Engineering.Application.Services.TelegramChats.TelegramServices;
using Engineering.Domain.Entities.DailyProjectOperations;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.DailyProjectOperations;

public partial class DailyProjectOperationLogic
{

    public async Task<string> TelegramDailyModel(
        DailyProjectOperation dailyProjectOperation,
        ProjectOperationDetail pODetail,
        bool isUpdate,
        string? unit,
        CT ct)
    {
        var created = dailyProjectOperation.Created;
        var tehranTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
        var createDateShamsi = created.ToShamsi();
        var createTime = TimeZoneInfo.ConvertTime(created, tehranTimeZone).ToString("HH:mm:ss");
        var startDateShamsi = TimeCalculator.ConvertToShamsi(dailyProjectOperation.StartDate);

        var creator = await _tpRepo.GetByUserIds([dailyProjectOperation.CreatorId], ct);
        var createdBy = creator.FirstOrDefault();

        string message = string.Empty;
        message = $"<b>سیستم گیتا</b> {Environment.NewLine}";
        message += isUpdate
            ? $"{TelegramValues.EditIcon}<b>ویرایش کارکرد روزانه</b>{Environment.NewLine}{Environment.NewLine}"
            : $"{TelegramValues.AddIcon}<b>ثبت کارکرد روزانه</b>{Environment.NewLine}{Environment.NewLine}";

        message += $"<b>مرکز هزینه :</b> {pODetail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterName} {Environment.NewLine}" +
                   $"<b>پروژه :</b> {pODetail.ProjectOperation.Project.ProjectName} {Environment.NewLine}" +
                   $"<b>شرح عملیات :</b> {pODetail.ProjectOperation.OperationInfo.OperationInfoName} {Environment.NewLine}" +
                   $"<b>موقعیت :</b> {pODetail.OperationLocation.PrivateName} {Environment.NewLine}" +
                   $"<b>توضیح :</b> {dailyProjectOperation.Description} {Environment.NewLine}" +
                   $"<b>حجم :</b> {Math.Round(dailyProjectOperation.FinalAmount, 5)} {unit} {Environment.NewLine}{Environment.NewLine}" +
                   $"{TelegramValues.CalendarIcon}<b>تاریخ کارکرد :</b> {startDateShamsi} {Environment.NewLine}" +
                   $"{TelegramValues.CalendarIcon}<b>تاریخ ثبت :</b> {createTime} {createDateShamsi} {Environment.NewLine}{Environment.NewLine}" +
                   $"{TelegramValues.UserIcon}<b>ثبت کننده :</b> {createdBy?.FirstName} {createdBy?.LastName} {Environment.NewLine}";

        return message;
    }
}
