namespace Engineering.Application.Services.DailyProjectOperations.Models.UpdateDailyProjectOperation;

public class UpdateDailyProjectOperationValidator : AbstractValidator<UpdateDailyProjectOperationRequest>
{
    public UpdateDailyProjectOperationValidator()
    {
        //RuleFor(oo => oo.StartDate).NotNull().WithError(DailyProjectOperationErrors.InValidStartDate);
        //RuleFor(oo => oo.EndDate).NotNull().WithError(DailyProjectOperationErrors.InValidEndDate);
        //RuleFor(oo => oo.StartDate.Date).Equal(oo => oo.EndDate.Date).WithError(DailyProjectOperationErrors.StartDateBiggerThanEndDate);
        RuleFor(oo => oo.Status).NotNull().WithError(GlobalErrors.StatusIsNull).IsInEnum().WithError(GlobalErrors.StatusNotInEnum);
        RuleFor(oo => oo.Length).NotNull().WithError(DailyProjectOperationErrors.InValidLength);
        RuleFor(oo => oo.Width).NotNull().WithError(DailyProjectOperationErrors.InValidWidth);
        RuleFor(oo => oo.Height).NotNull().WithError(DailyProjectOperationErrors.InValidHeight);
        RuleFor(oo => oo.Weight).NotNull().WithError(DailyProjectOperationErrors.InValidWeight);
        RuleFor(oo => oo.Number).NotNull().WithError(DailyProjectOperationErrors.InValidNumber);
        RuleFor(oo => oo.DailyProjectOperationId).NotNull().WithError(DailyProjectOperationErrors.InValidDailyProjectOperationId);
    }
}
