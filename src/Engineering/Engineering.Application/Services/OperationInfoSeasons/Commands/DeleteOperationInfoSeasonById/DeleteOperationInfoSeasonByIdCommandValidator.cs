namespace Engineering.Application.Services.OperationInfoSeasons.Commands.DeleteOperationInfoSeasonById;

public class DeleteOperationInfoSeasonByIdCommandValidator : AbstractValidator<DeleteOperationInfoSeasonByIdCommand>
{
    public DeleteOperationInfoSeasonByIdCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoSeasonErrors.IdIsEmpty);
    }
}
