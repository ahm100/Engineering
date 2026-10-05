namespace Engineering.Application.Services.Projects.Models.UpdateProjectProductQuantities;


public class UpdateProjectProductQuantitiesValidator : AbstractValidator<UpdateProjectProductQuantitiesRequest>
{
    public UpdateProjectProductQuantitiesValidator()
    {
        RuleFor(x => x.ProjectProductId).IsPositive(ProjectCmts.ProjectProductId);
        RuleFor(x => x.CompletedQuantity).IsPositive(ProjectCmts.CompletedQuantity);
        RuleFor(x => x.InProgressQuantity).IsPositive(ProjectCmts.InProgressQuantity);
    }
}

