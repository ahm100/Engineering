namespace Engineering.Application.Services.DailyProjectOperations.Commands.CreateDailyProjectOperationService;

public class CreateDailyProjectOperationServiceCommandValidator : AbstractValidator<CreateDailyProjectOperationServiceCommand>
{
    public CreateDailyProjectOperationServiceCommandValidator()
    {
        RuleFor(oo => oo.DailyProjectOperation).NotNull().WithError(DailyProjectOperationServiceErrors.InValidDailyProjectOperation);
        RuleFor(oo => oo.ContractorService).NotNull().WithError(DailyProjectOperationServiceErrors.InValidContractorService);
    }
}
