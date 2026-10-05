namespace Engineering.Application.Services.EmployerContracts.Contracts.UpdateEContractVolume;

public class UpdateEContractVolumeRequest : IHttpRequest
{
    public long Id { get; set; }
    public decimal? VolumeTolerance { get; set; }
    public List<UpdateEContractVolumeModel>? EContractVolumeModel { get; set; }
};

public class UpdateEContractVolumeModel
{
    public long Id { get; set; }
    public List<UpdateProjectOperationVolumeModel>? ProjectOperationVolumeModel { get; set; }
};

public class UpdateProjectOperationVolumeModel
{
    public long Id { get; set; }
    public decimal Workload { get; set; }
    public string? Description { get; set; }

};