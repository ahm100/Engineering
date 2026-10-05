using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.ModelValidator;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.CreateVizardProjectOperationDetail;

public class CreateVizardProjectOperationDetailRequestModelValidator : AbstractValidator<CreateVizardProjectOperationDetailRequestModel>
{
    public CreateVizardProjectOperationDetailRequestModelValidator()
    {
        RuleForEach(oo => oo.ProjectOperationIds).GreaterThanOrEqualTo(1).NotEmpty().WithError(ProjectOperationDetailErrors.ProjectOperationIdIsEmpty);
        RuleFor(oo => oo.Length).NotNull().GreaterThan(0).WithError(ProjectOperationDetailErrors.LengthIsEmpty);
        RuleFor(oo => oo.LengthChangeable).NotNull().WithError(ProjectOperationDetailErrors.LengthChangeableIsEmpty);
        RuleFor(oo => oo.Width).NotNull().GreaterThan(0).WithError(ProjectOperationDetailErrors.WidthIsEmpty);
        RuleFor(oo => oo.WidthChangeable).NotNull().WithError(ProjectOperationDetailErrors.WidthChangeableIsEmpty);
        RuleFor(oo => oo.Height).NotNull().GreaterThan(0).WithError(ProjectOperationDetailErrors.HeightIsEmpty);
        RuleFor(oo => oo.HeightChangeable).NotNull().WithError(ProjectOperationDetailErrors.HeightChangeableIsEmpty);
        RuleFor(oo => oo.Weight).NotNull().GreaterThan(0).WithError(ProjectOperationDetailErrors.WeightIsEmpty);
        RuleFor(oo => oo.WeightChangeable).NotNull().WithError(ProjectOperationDetailErrors.WeightChangeableIsEmpty);
        RuleFor(oo => oo.Number).NotNull().GreaterThan(0).WithError(ProjectOperationDetailErrors.NumberIsEmpty);
        RuleFor(oo => oo.NumberChangeable).NotNull().WithError(ProjectOperationDetailErrors.NumberChangeableIsEmpty);
        RuleFor(oo => oo.Priority).NotNull().GreaterThan(0).WithError(ProjectOperationDetailErrors.PriorityIsEmpty);
        When(oo => oo.ContractorServiceRequests != null, () =>
        {
            RuleForEach(oo => oo.ContractorServiceRequests).NotEmpty().SetValidator(new CreateContractorServiceRequestModelValidator());
        });
        When(oo => oo.ExpertRequests != null, () =>
        {
            RuleForEach(oo => oo.ExpertRequests).NotEmpty().SetValidator(new CreateExpertRequestModelValidator());
        });
        When(oo => oo.MachineryRequests != null, () =>
        {
            RuleForEach(oo => oo.MachineryRequests).NotEmpty().SetValidator(new CreateMachineryRequestModelValidator());
        });
        When(oo => oo.ProductRequests != null, () =>
        {
            RuleForEach(oo => oo.ProductRequests).NotEmpty().SetValidator(new CreateProductRequestModelValidator());
        });
        When(oo => oo.DeductionRequests != null, () =>
        {
            RuleForEach(oo => oo.DeductionRequests).NotEmpty().SetValidator(new CreateDeductionRequestModelValidator());
        });

        When(oo => oo.StartDate != null || oo.EndDate != null, () =>
        {
            RuleFor(oo => oo.StartDate).NotEmpty().WithError(GlobalErrors.StartDateIsNull);
            RuleFor(oo => oo.EndDate).NotEmpty().WithError(GlobalErrors.EndDateIsNull);
            RuleFor(oo => oo.StartDate!.Value.Date).LessThanOrEqualTo(oo => oo.EndDate!.Value.Date).WithError(GlobalErrors.StartDateCanNotBigerToEndDate);
            RuleFor(oo => oo.EndDate!.Value.Date).GreaterThanOrEqualTo(oo => oo.StartDate!.Value.Date).WithError(GlobalErrors.EndDateCanNotSmallerToStartDate);
        });
    }
}

