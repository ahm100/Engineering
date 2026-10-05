namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByCode;

public class GetOperationInfoByCodeQueryValidator : AbstractValidator<GetOperationInfoByCodeQuery>
{
    public GetOperationInfoByCodeQueryValidator()
    {
        RuleFor(oo => oo.OperationInfoCode).NotEmpty().WithError(OperationInfoErrors.OperationInfoCodeIsEmpty);
    }
}