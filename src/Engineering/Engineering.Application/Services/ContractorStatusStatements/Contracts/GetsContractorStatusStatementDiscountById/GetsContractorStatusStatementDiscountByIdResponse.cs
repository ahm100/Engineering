
namespace Engineering.Application.Services.ContractorStatusStatements.Models.GetsContractorStatusStatementDiscountById;

public record GetsContractorStatusStatementDiscountByIdResponse(
    List<GetsContractorStatusStatementDiscountByIdModel> Data,
    int RowCount
    );

public record GetsContractorStatusStatementDiscountByIdModel
{
    public long Id { get; set; }
    public DateTime? RegistrationDate { get; set; }
    public decimal DiscountPrice { get; set; }
    public string? Description { get; set; } = string.Empty;
}
