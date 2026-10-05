using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Application.Services.ProjectOperations.Commands.ProjectOperationWorkloder;

public record ProjectOperationWorkloderCommand(
    long Id,
    List<ProjectOperationDetailList> ProjectOperationDetails
    ) : ICommand<ProjectOperation>;

public record ProjectOperationWorkloderModel
{
    public long Id { get; set; }
    public List<ProjectOperationWorkloderDTO>? ProjectOperationDetailLists { get; set; }
}

public record ProjectOperationWorkloderDTO
{
    public long Id { get; set; }
    public decimal FinalAmount { get; set; }
}


public record ProjectOperationDetailList(
    long Id,
    decimal FinalAmount,
    bool IsNew
    );

