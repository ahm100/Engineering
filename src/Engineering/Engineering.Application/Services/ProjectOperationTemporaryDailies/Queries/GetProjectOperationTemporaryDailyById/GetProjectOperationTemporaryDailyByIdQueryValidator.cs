namespace Engineering.Application.Services.ProjectOperationTemporaryDailies.Queries.GetProjectOperationTemporaryDailyById;

public class GetProjectOperationTemporaryDailyByIdQueryValidator : AbstractValidator<GetProjectOperationTemporaryDailyByIdQuery>
{
    public GetProjectOperationTemporaryDailyByIdQueryValidator()
    {
        RuleFor(oo => oo.ProjectOperationTemporaryDailyId).NotNull().WithError(ProjectOperationTemporaryDailyErrors.IdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
