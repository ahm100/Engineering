
namespace Engineering.Application.Services.EmployerContracts.Contracts.CreateEContract;

public class CreateEContractValidator : AbstractValidator<CreateEContractRequest>
{
    public CreateEContractValidator()
    {
        RuleFor(c => c.EContractHeaderId)
            .IsPositive(EContractCmts.EmployerContractHead);

        RuleForEach(oo => oo.CreateContracts)
            .NotEmpty().SetValidator(new CreateEContractModelValidator());
    }
}

public class CreateEContractModelValidator : AbstractValidator<CreateEContractModel>
{
    public CreateEContractModelValidator()
    {

        RuleFor(c => c.StartDate)
            .LessThanPropertyDate(x => x.EndDate, GlobalCmts.StartDate, GlobalCmts.EndDate);

        RuleFor(c => c.EndDate)
            .GreaterThanPropertyDate(x => x.StartDate, GlobalCmts.StartDate, GlobalCmts.EndDate);

        When(oo => !string.IsNullOrEmpty(oo.Description), () =>
        {
            RuleFor(c => c.Description)
                .IsFullString(GlobalCmts.Description, 1500, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
        });

        When(oo => oo.CreateDocuments != null && oo.CreateDocuments.Any(), () =>
        {
            RuleForEach(oo => oo.CreateDocuments)
                .NotEmpty().SetValidator(new CreateEmployerDocModelValidator());
        });

        When(oo => oo.CreateConsiderations != null && oo.CreateConsiderations.Any(), () =>
        {
            RuleForEach(oo => oo.CreateConsiderations)
                .NotEmpty().SetValidator(new CreateEmployerConsiderationModelValidator());
        });

        When(oo => oo.AssigneOperations != null && oo.AssigneOperations.Any(), () =>
        {
            RuleForEach(oo => oo.AssigneOperations)
                .NotEmpty().SetValidator(new CreateEmployerOperationModelValidator());
        });

        When(oo => oo.CreateOperations != null && oo.CreateOperations.Any(), () =>
        {
            RuleForEach(oo => oo.CreateOperations)
                .NotEmpty().SetValidator(new CreateProjectOperationModelValidator());
        });
    }
}

public class CreateEmployerDocModelValidator : AbstractValidator<CreateEmployerDocModel>
{
    public CreateEmployerDocModelValidator()
    {
        RuleFor(c => c.Type)
            .IsEnum(EContractCmts.EDocumentType);

        RuleFor(c => c.RegistrationDate)
            .IsDate(EContractCmts.RegistrationDate);

        RuleFor(c => c.Version)
            .IsPositive(GlobalCmts.Version);

        When(oo => !string.IsNullOrEmpty(oo.Description), () =>
        {
            RuleFor(c => c.Description)
                .IsFullString(GlobalCmts.Description, 1500, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
        });

        RuleForEach(oo => oo.Urls)
            .IsRequiredString(GlobalCmts.Url);
    }
}

public class CreateEmployerConsiderationModelValidator : AbstractValidator<CreateEmployerConsiderationModel>
{
    public CreateEmployerConsiderationModelValidator()
    {
        RuleFor(c => c.Type)
            .IsEnum(EContractCmts.ConsiderationType);

        When(oo => !string.IsNullOrEmpty(oo.Description), () =>
        {
            RuleFor(c => c.Description)
                .IsFullString(GlobalCmts.Description, 1500, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
        });
    }
}

public class CreateEmployerOperationModelValidator : AbstractValidator<CreateEmployerOperationModel>
{
    public CreateEmployerOperationModelValidator()
    {
        RuleFor(c => c.ProjectOperationId)
            .IsPositive(GlobalCmts.ProjectOperationId);

        When(oo => !string.IsNullOrEmpty(oo.Description), () =>
        {
            RuleFor(c => c.Description)
                .IsFullString(GlobalCmts.Description, 1500, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
        });

        When(oo => oo.ProjectOperationDetailIds != null && oo.ProjectOperationDetailIds.Any(), () =>
        {
            RuleForEach(oo => oo.ProjectOperationDetailIds)
                .IsPositive(GlobalCmts.ProjectOperationDetailId);
        });
    }
}

public class CreateProjectOperationModelValidator : AbstractValidator<CreateProjectOperationModel>
{
    public CreateProjectOperationModelValidator()
    {
        RuleFor(c => c.OperationInfoId)
            .IsPositive(GlobalCmts.OperationInfoId);

        RuleFor(c => c.Workload)
            .IsPositive(POperationCmts.Workload);

        RuleFor(c => c.Priority)
            .IsPositiveWithNullableInput(POperationCmts.Priority);

        RuleFor(c => c.Status)
            .IsEnum(POperationCmts.Status);

        When(oo => oo.Urls != null && oo.Urls.Any(), () =>
        {
            RuleForEach(oo => oo.Urls)
                .IsRequiredString(GlobalCmts.Url);
        });

        When(oo => !string.IsNullOrEmpty(oo.Description), () =>
        {
            RuleFor(c => c.Description)
                .IsFullString(GlobalCmts.Description, 1500, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
        });

    }
}