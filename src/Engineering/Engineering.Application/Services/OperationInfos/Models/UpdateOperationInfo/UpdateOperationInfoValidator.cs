using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.ModelValidator;

namespace Engineering.Application.Services.OperationInfos.Models.UpdateOperationInfo;

public class UpdateOperationInfoValidator : AbstractValidator<UpdateOperationInfoRequest>
{
    public UpdateOperationInfoValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoErrors.IdIsEmpty);
        RuleFor(oo => oo.UnitOfMeasurementId).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoErrors.UnitOfMeasurementIdIsEmpty);
        RuleFor(oo => oo.OperationInfoName).NotEmpty().WithError(OperationInfoErrors.OperationInfoNameIsEmpty);
        RuleFor(oo => oo.OperationInfoCode).NotEmpty().WithError(OperationInfoErrors.OperationInfoCodeIsEmpty);
        RuleFor(oo => oo.OperationInfoName).Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);
        RuleFor(oo => oo.OperationInfoCode).Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);
        RuleFor(oo => oo.IsActive).NotNull().WithError(OperationInfoErrors.IsActiveIsEmpty);
        When(oo => oo.ExpertStandards != null, () =>
        {
            RuleForEach(oo => oo.ExpertStandards).NotEmpty().SetValidator(new UpdateOperationInfoExpertRequestModelValidator());
        });
        When(oo => oo.GoodsStandards != null, () =>
        {
            RuleForEach(oo => oo.GoodsStandards).NotEmpty().SetValidator(new UpdateOperationInfoGoodsRequestModelValidator());
        });
        When(oo => oo.MachineryStandards != null, () =>
        {
            RuleForEach(oo => oo.MachineryStandards).NotEmpty().SetValidator(new UpdateOperationInfoMachineriesRequestModelValidator());
        });
        When(oo => oo.ServiceInfos != null, () =>
        {
            RuleForEach(c => c.ServiceInfos).NotNull().WithError(OperationInfoErrors.ServiceInfoIdsIsEmpty);
        });
        When(oo => oo.OperationInfoDependency != null, () =>
        {
            RuleFor(c => c.OperationInfoDependency).NotEmpty().WithError(OperationInfoErrors.OperationInfoDependencyIsEmpty);
        });

    }
}
