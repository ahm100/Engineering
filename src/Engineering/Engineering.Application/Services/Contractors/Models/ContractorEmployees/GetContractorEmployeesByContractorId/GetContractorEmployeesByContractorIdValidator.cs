namespace Engineering.Application.Services.Contractors.Models.ContractorEmployees.GetContractorEmployeesByContractorId;

public class GetContractorEmployeesByContractorIdValidator : AbstractValidator<GetContractorEmployeesByContractorIdRequest>
{
    public GetContractorEmployeesByContractorIdValidator()
    {
        RuleFor(c => c.Id).NotNull().WithMessage("شناسه پیمانکار خالی می باشد");
    }
}
