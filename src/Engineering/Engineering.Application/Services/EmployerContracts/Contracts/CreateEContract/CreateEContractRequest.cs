using Engineering.Domain.Entities.EmployerContracts;
using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.EmployerContracts.Contracts.CreateEContract;

public class CreateEContractRequest : IHttpRequest
{
    public long EContractHeaderId { get; set; }
    public required List<CreateEContractModel> CreateContracts { get; set; }
};

public class CreateEContractModel
{
    public long ProjectId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public List<CreateEmployerDocModel>? CreateDocuments { get; set; }
    public List<CreateEmployerConsiderationModel>? CreateConsiderations { get; set; }
    public List<CreateEmployerOperationModel>? AssigneOperations { get; set; }
    public List<CreateProjectOperationModel>? CreateOperations { get; set; }
};

public class CreateEmployerDocModel
{
    public EDocumentType Type { get; set; }
    public DateTime RegistrationDate { get; set; }
    public decimal Version { get; set; } = 1;
    public required List<string> Urls { get; set; }
    public bool IsActive { get; set; }
    public string? Description { get; set; }
};

public class CreateEmployerConsiderationModel
{
    public string? FlagId { get; set; }
    public ConsiderationType Type { get; set; }
    public bool IsActive { get; set; }
    public string? Description { get; set; }
};

public class CreateEmployerOperationModel
{
    public List<string>? FlagIds { get; set; }
    public long ProjectOperationId { get; set; }
    public string? Description { get; set; }
    public List<long>? ProjectOperationDetailIds { get; set; }
};

public class CreateProjectOperationModel
{
    public List<string>? FlagIds { get; set; }
    public long OperationInfoId { get; set; }
    public decimal Workload { get; set; }
    public int? Priority { get; set; }
    public ProjectOperationStatus Status { get; set; }
    public bool GoodsInProgress { get; set; } = false;
    public DateTime? BaselineStartDate { get; set; }
    public DateTime? BaselineFinishDate { get; set; }
    public List<string>? Urls { get; set; }
    public string? Description { get; set; }
};