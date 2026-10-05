namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetsDailyProjectOperationDocument;

public class GetsDailyProjectOperationDocumentQueryValidator : AbstractValidator<GetsDailyProjectOperationDocumentQuery>
{
    public GetsDailyProjectOperationDocumentQueryValidator()
    {
        RuleFor(oo => oo.DailyProjectOperationId).NotNull().WithError(DailyProjectOperationErrors.InValidDailyProjectOperationId);
    }
}
