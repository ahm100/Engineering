namespace Engineering.Application.Services.DailyProjectOperations.Models.UpdateDailyProjectOperationDocuments;

public class UpdateDailyProjectOperationDocumentsValidator : AbstractValidator<UpdateDailyProjectOperationDocumentsRequest>
{
    public UpdateDailyProjectOperationDocumentsValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(DailyProjectOperationErrors.InValidDailyProjectOperationId);
    }
}
