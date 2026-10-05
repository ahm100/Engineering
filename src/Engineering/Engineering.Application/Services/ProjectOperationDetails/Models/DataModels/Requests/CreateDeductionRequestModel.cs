namespace Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Requests;

public record CreateDeductionRequestModel
{
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public decimal Number { get; set; }
}


