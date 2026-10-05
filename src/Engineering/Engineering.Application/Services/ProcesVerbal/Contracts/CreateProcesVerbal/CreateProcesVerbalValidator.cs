using Engineering.Domain.Entities.ProcesVerbal.Enums;

namespace Engineering.Application.Services.ProcesVerbal.Contracts.CreateProcesVerbal;

public class CreateProcesVerbalValidator : AbstractValidator<CreateProcesVerbalRequest>
{
    public CreateProcesVerbalValidator()
    {
        RuleFor(oo => oo.TitleFa)
            .IsFullString(ProcesVerbalCmts.TitleFa, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

        RuleFor(oo => oo.TitleEn)
            .MaximumLength(250)
            .WithError(GlobalErrors.RequiredMaxLength(GlobalCmts.TitleEn, 250))
            .When(oo => oo.TitleEn != null);

        RuleFor(oo => oo.Type)
            .IsEnum(ProcesVerbalCmts.Type);

        RuleFor(oo => oo.ProjectId)
            .IsPositive(GlobalCmts.ProjectId);

        RuleFor(oo => oo.ContractId)
            .IsPositive(GlobalCmts.ContractId);

        RuleFor(oo => oo.RecordDateTime)
            .IsDate(ProcesVerbalCmts.RecordDateTime);

        RuleFor(oo => oo.Location)
            .MaximumLength(500)
            .WithError(GlobalErrors.RequiredMaxLength(ProcesVerbalCmts.Location, 500))
            .When(oo => oo.Location != null);

        RuleFor(oo => oo.DeliveryStatus)
            .IsNullableEnum(ProcesVerbalCmts.DeliveryStatus);

        RuleFor(oo => oo.WorkStatus)
            .IsNullableEnum(ProcesVerbalCmts.WorkStatus);

        RuleFor(oo => oo.LimitationStatus)
            .IsNullableEnum(ProcesVerbalCmts.LimitationStatus);

        RuleFor(oo => oo.WorkStartStatus)
            .IsNullableEnum(ProcesVerbalCmts.WorkStartStatus);

        RuleFor(oo => oo.WorkStopReason)
            .IsNullableEnum(ProcesVerbalCmts.WorkStopReason);

        RuleFor(oo => oo.WorkStopStatus)
            .IsNullableEnum(ProcesVerbalCmts.WorkStopStatus);

        RuleFor(oo => oo.Docs)
            .NotNull().WithError(GlobalErrors.RequiredNull(ProcesVerbalCmts.ProcesVerbalDoc))
            .Must(docs => docs is { Count: >= 2 })
            .HasNoDuplicates(url => url, ProcesVerbalCmts.ProcesVerbalDoc);

        RuleForEach(oo => oo.Docs)
            .IsRequiredString(GlobalCmts.URL);

        RuleFor(oo => oo.Items)
            .NotNull().WithError(GlobalErrors.RequiredNull(ProcesVerbalCmts.ProcesVerbalItem))
            .Must(items => items is { Count: > 0 })
            .HasNoDuplicates(item => item, ProcesVerbalCmts.ProcesVerbalItem);

        RuleForEach(oo => oo.Items)
            .IsRequiredString(ProcesVerbalCmts.ProcesVerbalItemTitle);

        RuleFor(oo => oo.DeliveryStatus)
            .NotNull().WithError(GlobalErrors.RequiredNull(ProcesVerbalCmts.DeliveryStatus))
            .When(oo => oo.Type == ProcesVerbalType.LandDelivery);

        RuleFor(oo => oo.DeliveryStatus)
            .NotNull().WithError(GlobalErrors.RequiredNull(ProcesVerbalCmts.DeliveryStatus))
            .When(oo => oo.Type == ProcesVerbalType.WorkshopDelivery);

        RuleFor(oo => oo.Products)
            .NotNull().WithError(GlobalErrors.RequiredNull(ProcesVerbalCmts.ProcesVerbalProduct))
            .Must(products => products is { Count: > 0 })
            .When(oo => oo.Type == ProcesVerbalType.WorkshopEquipping);

        RuleFor(oo => oo.WorkStatus)
            .NotNull().WithError(GlobalErrors.RequiredNull(ProcesVerbalCmts.WorkStatus))
            .When(oo => oo.Type == ProcesVerbalType.TempDelivery);

        RuleFor(oo => oo.LimitationStatus)
            .NotNull().WithError(GlobalErrors.RequiredNull(ProcesVerbalCmts.LimitationStatus))
            .When(oo => oo.Type == ProcesVerbalType.FinalDelivery);

        RuleFor(oo => oo.PODs)
            .NotNull().WithError(GlobalErrors.RequiredNull(ProcesVerbalCmts.ProcesVerbalPOD))
            .Must(pods => pods is { Count: > 0 })
            .When(oo => oo.Type == ProcesVerbalType.ChangingValues);

        RuleFor(oo => oo.PODs)
            .HasNoDuplicates(pod => pod.PODId, ProcesVerbalCmts.ProcesVerbalPOD);

        RuleFor(oo => oo.Products)
            .HasNoDuplicates(product => product.ProductId, ProcesVerbalCmts.ProcesVerbalProduct);

        RuleFor(oo => oo.WorkStartStatus)
            .NotNull().WithError(GlobalErrors.RequiredNull(ProcesVerbalCmts.WorkStartStatus))
            .When(oo => oo.Type == ProcesVerbalType.WorkStart);

        RuleFor(oo => oo.WorkStopReason)
            .NotNull().WithError(GlobalErrors.RequiredNull(ProcesVerbalCmts.WorkStopReason))
            .When(oo => oo.Type == ProcesVerbalType.WorkStop);

        RuleFor(oo => oo.WorkStopStatus)
            .NotNull().WithError(GlobalErrors.RequiredNull(ProcesVerbalCmts.WorkStopStatus))
            .When(oo => oo.Type == ProcesVerbalType.WorkStop);

        RuleForEach(oo => oo.PODs).ChildRules(pod =>
        {
            pod.RuleFor(p => p.PODId)
                .IsPositive(GlobalCmts.Id);

            pod.RuleFor(p => p.NewFinalAmount)
                .IsPositive(ProcesVerbalCmts.NewFinalAmout)
                .PrecisionScale(18, 2, true);
        });

        RuleForEach(oo => oo.Products).ChildRules(product =>
        {
            product.RuleFor(p => p.ProductId)
                .IsPositive(GlobalCmts.Id);

            product.RuleFor(p => p.NewFinalAmount)
                .IsPositive(ProcesVerbalCmts.NewFinalAmout)
                .PrecisionScale(18, 2, true);

            product.RuleFor(p => p.Status)
                .IsEnum(ProcesVerbalCmts.ProcesVerbalProductItemStatus);

            product.RuleFor(p => p.Description)
                .IsFullString(GlobalCmts.Description, 1500, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
        });
    }
}
