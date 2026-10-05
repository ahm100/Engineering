namespace Engineering.Application.Services.Contractors.Commands.ContractorEmployees.DeleteContractorEmployee;

public class DeleteContractorEmployeeCommandValidator : AbstractValidator<DeleteContractorEmployeeCommand>
{
    public DeleteContractorEmployeeCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("شناسه اجباری می باشد.");
    }
}
