namespace Engineering.Application.Services.OperationInfoDependencies.Models.GetOperationInfoDependencyById;

public class GetOperationInfoDependencyByIdValidator : AbstractValidator<GetOperationInfoDependencyByIdRequest>
{
    public GetOperationInfoDependencyByIdValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoDependencyErrors.IdIsEmpty);
    }
}
