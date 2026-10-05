namespace Engineering.Application.Services.ProcesVerbal.Contracts.GetProcesVerbals;

public class GetProcesVerbalsValidator : AbstractValidator<GetProcesVerbalsRequest>
{
    public GetProcesVerbalsValidator()
    {
        RuleFor(oo => oo.TargetId)
                    .GreaterThan(0)
                    .When(oo => oo.TargetId.HasValue)
                    .WithMessage("شناسه هدف باید بزرگتر از صفر باشد.");

        RuleFor(oo => oo.ContractId)
            .GreaterThan(0)
            .When(oo => oo.ContractId.HasValue)
            .WithMessage("شناسه قرارداد باید بزرگتر از صفر باشد.");

        RuleFor(oo => oo.ProjectId)
            .GreaterThan(0)
            .When(oo => oo.ProjectId.HasValue)
            .WithMessage("شناسه پروژه باید بزرگتر از صفر باشد.");

        RuleFor(oo => oo.Type)
            .IsInEnum()
            .When(oo => oo.Type.HasValue)
            .WithMessage("نوع صورتجلسه نامعتبر است.");

        RuleFor(oo => oo.Title)
            .MaximumLength(250)
            .WithMessage("عنوان نمی‌تواند بیشتر از 250 کاراکتر باشد.");

        RuleFor(oo => oo.PageIndex)
            .IsPositive(GlobalCmts.PageIndex);

        RuleFor(oo => oo.PageSize)
            .IsPositive(GlobalCmts.PageSize)
            .LessThanOrEqualTo(200)
            .WithMessage("اندازه صفحه نمی‌تواند بیشتر از ۲۰۰ باشد.");
    }
}
