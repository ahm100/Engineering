namespace Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.GetProjectOperationTemporaryDailyById;

public class GetProjectOperationTemporaryDailyByIdValidator : AbstractValidator<GetProjectOperationTemporaryDailyByIdRequest>
{
    public GetProjectOperationTemporaryDailyByIdValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ProjectOperationTemporaryDailyErrors.IdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
