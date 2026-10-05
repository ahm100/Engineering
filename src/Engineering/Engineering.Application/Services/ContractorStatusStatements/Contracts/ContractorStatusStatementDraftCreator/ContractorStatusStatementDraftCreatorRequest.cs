
namespace Engineering.Application.Services.ContractorStatusStatements.Models.ContractorStatusStatementDraftCreator;

public class ContractorStatusStatementDraftCreatorRequest : IHttpRequest
{
    public long ContractorId { get; set; }
    public long ProjectId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
};
