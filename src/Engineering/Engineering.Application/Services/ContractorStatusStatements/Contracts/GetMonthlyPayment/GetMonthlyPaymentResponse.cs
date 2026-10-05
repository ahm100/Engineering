namespace Engineering.Application.Services.ContractorStatusStatements.Contracts.GetMonthlyPayment;

public record GetMonthlyPaymentResponse(
    List<GetMonthlyPaymentModel> Data,
    int Count);

public record GetMonthlyPaymentModel
{
    public string MonthLabel { get; set; } = string.Empty;
    public decimal TotalPaid { get; set; }
}