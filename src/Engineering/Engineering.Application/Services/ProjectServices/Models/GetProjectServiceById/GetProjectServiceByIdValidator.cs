namespace Engineering.Application.Services.ProjectServices.Models.GetProjectServiceById;

public class GetProjectServiceByIdValidator : AbstractValidator<GetProjectServiceByIdRequest>
{
    public GetProjectServiceByIdValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ProjectServiceErrors.IdIsEmpty)
           .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
