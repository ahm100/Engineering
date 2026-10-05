namespace Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyProjectOperationDocument;

public class GetsDailyProjectOperationDocumentValidator : AbstractValidator<GetsDailyProjectOperationDocumentRequest>
{
    public GetsDailyProjectOperationDocumentValidator()
    {
        RuleFor(oo => oo.DailyProjectOperationId).NotNull().WithError(DailyProjectOperationErrors.InValidDailyProjectOperationId);
    }
}
