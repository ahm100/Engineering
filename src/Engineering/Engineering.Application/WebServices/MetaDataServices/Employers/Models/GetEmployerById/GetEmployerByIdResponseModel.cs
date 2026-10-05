using EmployerModel = Engineering.Application.WebServices.MetaDataServices.Employers.Models.Employer;

namespace Engineering.Application.WebServices.MetaDataServices.Employers.Models.GetEmployerById;

public class GetEmployerByIdResponseModel
{
    [JsonProperty("data")]
    public List<EmployerModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
