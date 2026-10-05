namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByName;

public class GetOperationInfoByNameQueryValidator : AbstractValidator<GetOperationInfoByNameQuery>
{
    public GetOperationInfoByNameQueryValidator()
    {
        RuleFor(oo => oo.OperationInfoName).NotEmpty().WithError(OperationInfoErrors.OperationInfoNameIsEmpty);
    }
}