namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetProjectOperationDetailCommentById;

public class GetProjectOperationDetailCommentByIdValidator : AbstractValidator<GetProjectOperationDetailCommentByIdRequest>
{
    public GetProjectOperationDetailCommentByIdValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetailId).NotNull().WithError(ProjectOperationDetailCommentErrors.UnValidProjectOperationDetailId);
    }
}

