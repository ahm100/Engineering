using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.ModelValidator;
using Engineering.Domain.Errors.Actions;

namespace Engineering.Application.Services.OperationInfos.Models.CreateOperationInfo;

public class CreateOperationInfoValidator : AbstractValidator<CreateOperationInfoRequest>
{
    public CreateOperationInfoValidator()
    {
        RuleFor(c => c.SeasonIds)
            .NotEmpty().WithError(OperationInfoErrors.SeasonIdsIsEmpty)
            .NotNull().WithError(OperationInfoErrors.SeasonIdsIsNull);

        RuleForEach(c => c.SeasonIds)
            .GreaterThan(0).WithError(OperationInfoErrors.SeasonIdsLessThanOrEqualZero);

        RuleFor(oo => oo.UnitOfMeasurementId)
            .NotNull()
            .GreaterThanOrEqualTo(1).WithError(OperationInfoErrors.UnitOfMeasurementIdIsEmpty);

        RuleFor(oo => oo.OperationInfoName)
            .NotEmpty().WithError(OperationInfoErrors.OperationInfoNameIsEmpty);

        RuleFor(oo => oo.OperationInfoName)
            .Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);

        RuleFor(oo => oo.OperationInfoCode)
            .NotEmpty().WithError(OperationInfoErrors.OperationInfoCodeIsEmpty);

        RuleFor(oo => oo.OperationInfoCode)
            .Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);

        RuleFor(oo => oo.IsActive)
            .NotNull().WithError(OperationInfoErrors.IsActiveIsEmpty);

        When(oo => oo.ExpertStandards != null, () =>
        {
            RuleForEach(oo => oo.ExpertStandards).NotEmpty().SetValidator(new OperationInfoExpertRequestModelValidator());
        });

        When(oo => oo.GoodsStandards != null, () =>
        {
            RuleForEach(oo => oo.GoodsStandards).NotEmpty().SetValidator(new OperationInfoGoodsRequestModelValidator());
        });

        When(oo => oo.MachineryStandards != null, () =>
        {
            RuleForEach(oo => oo.MachineryStandards).NotEmpty().SetValidator(new OperationInfoMachineryRequestModelValidator());
        });

        When(oo => oo.OperationInfoActions != null, () =>
        {
            RuleForEach(oo => oo.OperationInfoActions).NotEmpty().SetValidator(new CreateOperationInfoActionsModelValidator());
        });

        When(oo => oo.ServiceInfos != null, () =>
        {
            RuleForEach(c => c.ServiceInfos).NotEmpty().WithError(OperationInfoErrors.ServiceInfoIdsIsEmpty);
        });

        When(oo => oo.OperationInfoDependency != null, () =>
        {
            RuleFor(c => c.OperationInfoDependency).NotEmpty().WithError(OperationInfoErrors.OperationInfoDependencyIsEmpty);
        });
    }
}

public class CreateOperationInfoActionsModelValidator : AbstractValidator<CreateOperationInfoActionsModel>
{
    public CreateOperationInfoActionsModelValidator()
    {
        RuleFor(c => c.ActionId)
            .IsPositive(ActionErrors.IdIsEmpty);
        RuleFor(c => c.Price)
            .IsPositive(ActionErrors.PriceShouldBePositive);
    }
}
