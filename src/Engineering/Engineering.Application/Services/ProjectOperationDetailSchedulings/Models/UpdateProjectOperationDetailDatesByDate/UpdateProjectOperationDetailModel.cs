namespace Engineering.Application.Services.ProjectOperationDetailSchedulings.Models.UpdateProjectOperationDetailDatesByDate;

public record UpdateProjectOperationDetailModel
{
    public long Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

}
