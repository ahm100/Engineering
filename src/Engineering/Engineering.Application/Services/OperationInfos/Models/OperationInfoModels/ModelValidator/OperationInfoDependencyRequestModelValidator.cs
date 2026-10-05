using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.OperationInfoDependency;

namespace Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.ModelValidator;

public class OperationInfoDependencyRequestModelValidator : AbstractValidator<OperationInfoDependencyRequestModel>
{
    public OperationInfoDependencyRequestModelValidator()
    {
        RuleFor(oo => oo.WorkingDays).NotNull().WithError(OperationInfoDependencyErrors.WorkingDaysIsEmpty);
        RuleFor(oo => oo.RelationId).NotNull().GreaterThanOrEqualTo(1)
            .WithError(OperationInfoDependencyErrors.RelationIdIsEmpty);
        RuleFor(oo => oo.DependencyType).NotNull().WithError(OperationInfoDependencyErrors.DependencyTypeIsEmpty);

    }
}
