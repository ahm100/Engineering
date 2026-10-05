using Engineering.Application.Services.EmployerContracts.Contracts.CreateEContract;
using Engineering.Domain.Entities.EmployerContracts;
using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.EmployerContracts.Contracts.UpdateEContract;

public class UpdateEContractRequest : IHttpRequest
{
    public long Id { get; set; }
    public DateTime StartDate { get; set; }
    public string? Code { get; set; }
    public DateTime EndDate { get; set; }
    public string? Description { get; set; }
    public List<CreateEmployerDocModel>? CreateDocuments { get; set; }
    public List<UpdateEmployerDocModel>? UpdateDocuments { get; set; }
    public List<long>? DeleteDocuments { get; set; }
    public List<CreateEmployerConsiderationModel>? CreateConsiderations { get; set; }
    public List<UpdateEmployerConsiderationModel>? UpdateConsiderations { get; set; }
    public List<long>? DeleteConsiderations { get; set; }
    public List<CreateEmployerOperationModel>? AssigneOperations { get; set; }
    public List<CreateProjectOperationModel>? CreateOperations { get; set; }
    public List<UpdateProjectOperationModel>? UpdateOperations { get; set; }
    public List<long>? DeleteOperations { get; set; }
};

public class UpdateEmployerDocModel
{
    public long Id { get; set; }
    public EDocumentType Type { get; set; }
    public DateTime RegistrationDate { get; set; }
    public decimal Version { get; set; }
    public required List<string> Urls { get; set; }
    public bool IsActive { get; set; }
    public string? Description { get; set; }
};

public class UpdateEmployerConsiderationModel
{
    public long Id { get; set; }
    public string? FlagId { get; set; }
    public ConsiderationType Type { get; set; }
    public bool IsActive { get; set; }
    public string? Description { get; set; }
};

public class UpdateProjectOperationModel
{
    public long Id { get; set; }
    public List<string>? FlagIds { get; set; }
    public long OperationInfoId { get; set; }
    public decimal Workload { get; set; }
    public decimal TolerancePercentage { get; set; }
    public decimal Price { get; set; }
    public decimal ChangedPrice { get; set; }
    public int? Priority { get; set; }
    public ProjectOperationStatus Status { get; set; }
    public bool GoodsInProgress { get; set; } = false;
    public List<string>? Urls { get; set; }
    public string? Description { get; set; }
    public List<long>? ProjectOperationDetailIds { get; set; }
};