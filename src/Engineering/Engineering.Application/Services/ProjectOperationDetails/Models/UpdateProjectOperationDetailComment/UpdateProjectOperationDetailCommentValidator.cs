namespace Engineering.Application.Services.ProjectOperationDetails.Models.UpdateProjectOperationDetailComment;

public class UpdateProjectOperationDetailCommentValidator : AbstractValidator<UpdateProjectOperationDetailCommentRequest>
{
    public UpdateProjectOperationDetailCommentValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ProjectOperationDetailCommentErrors.UnValidProjectOperationDetailCommentId);
    }
}
