namespace Engineering.Application.Services.TelegramChats.Queries.GetByProjectOperationDetailId;

public class GetByProjectOperationDetailIdQueryValidator : AbstractValidator<GetByProjectOperationDetailIdQuery>
{
    public GetByProjectOperationDetailIdQueryValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetailId).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ProjectOperationDetailErrors.IdIsEmpty);
    }
}