using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Application.Services.ProjectWbses.Commands.EditTaskActualDateTimes
{
    public class EditTaskActualDateTimesCommandValidator : AbstractValidator<EditTaskActualDateTimesCommand>
    {
        public EditTaskActualDateTimesCommandValidator()
        {
            RuleFor(x => x.TaskId)
                .IsPositive(GlobalCmts.Id)
                .WithError(GlobalErrors.IdsLessThanOrEqualZero);
        }
    }
}
