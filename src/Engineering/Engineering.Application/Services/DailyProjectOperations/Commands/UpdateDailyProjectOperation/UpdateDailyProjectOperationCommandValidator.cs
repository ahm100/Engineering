namespace Engineering.Application.Services.DailyProjectOperations.Commands.UpdateDailyProjectOperation;

public class UpdateDailyProjectOperationCommandValidator : AbstractValidator<UpdateDailyProjectOperationCommand>
{
    public UpdateDailyProjectOperationCommandValidator()
    {
        RuleFor(oo => oo.DailyProjectOperationId).NotNull().WithError(DailyProjectOperationErrors.InValidDailyProjectOperationId);
        //RuleFor(oo => oo.StartDate).NotNull().WithError(DailyProjectOperationErrors.InValidStartDate);
        //RuleFor(oo => oo.EndDate).NotNull().WithError(DailyProjectOperationErrors.InValidEndDate);
        //RuleFor(oo => oo.StartDate.Date).Equal(oo => oo.EndDate.Date).WithError(DailyProjectOperationErrors.StartDateBiggerThanEndDate);
        RuleFor(oo => oo.Status).NotNull().WithError(DailyProjectOperationErrors.InValidStatus);
        RuleFor(oo => oo.Length).NotNull().WithError(DailyProjectOperationErrors.InValidLength);
        RuleFor(oo => oo.Width).NotNull().WithError(DailyProjectOperationErrors.InValidWidth);
        RuleFor(oo => oo.Height).NotNull().WithError(DailyProjectOperationErrors.InValidHeight);
        RuleFor(oo => oo.Weight).NotNull().WithError(DailyProjectOperationErrors.InValidWeight);
        RuleFor(oo => oo.Number).NotNull().WithError(DailyProjectOperationErrors.InValidNumber);
    }
}
