using Engineering.Application.Services.EmployerContracts.Contracts.CreateEContract;

namespace Engineering.Application.Services.EmployerContracts.Contracts.UpdateEContract;


public class UpdateEContractValidator : AbstractValidator<UpdateEContractRequest>
{
    public UpdateEContractValidator()
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

        When(oo => oo.UpdateDocuments != null && oo.UpdateDocuments.Any(), () =>
        {
            RuleForEach(oo => oo.UpdateDocuments)
                .NotEmpty().SetValidator(new UpdateEmployerDocModelValidator());
        });

        When(oo => oo.CreateDocuments != null && oo.CreateDocuments.Any(), () =>
        {
            RuleForEach(oo => oo.CreateDocuments)
                .NotEmpty().SetValidator(new CreateEmployerDocModelValidator());
        });

        When(oo => oo.UpdateConsiderations != null && oo.UpdateConsiderations.Any(), () =>
        {
            RuleForEach(oo => oo.UpdateConsiderations)
                .NotEmpty().SetValidator(new UpdateEmployerConsiderationModelValidator());
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

        When(oo => oo.UpdateOperations != null && oo.UpdateOperations.Any(), () =>
        {
            RuleForEach(oo => oo.UpdateOperations)
                .NotEmpty().SetValidator(new UpdateProjectOperationModelValidator());
        });
    }
}

public class UpdateEmployerDocModelValidator : AbstractValidator<UpdateEmployerDocModel>
{
    public UpdateEmployerDocModelValidator()
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

public class UpdateEmployerConsiderationModelValidator : AbstractValidator<UpdateEmployerConsiderationModel>
{
    public UpdateEmployerConsiderationModelValidator()
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

public class UpdateProjectOperationModelValidator : AbstractValidator<UpdateProjectOperationModel>
{
    public UpdateProjectOperationModelValidator()
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