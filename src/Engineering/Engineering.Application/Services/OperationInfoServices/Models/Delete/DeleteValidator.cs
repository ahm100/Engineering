namespace Engineering.Application.Services.OperationInfoServices.Models.DeleteOperationInfoService;

public class DeleteOperationInfoServiceValidator : AbstractValidator<DeleteOperationInfoServiceRequest>
{
    public DeleteOperationInfoServiceValidator()
    {
        RuleFor(oo => oo.ServiceInfoId).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoServiceErrors.IdIsEmpty);
    }
}
