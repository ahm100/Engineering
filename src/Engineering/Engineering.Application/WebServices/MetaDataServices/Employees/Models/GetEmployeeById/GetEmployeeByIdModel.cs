using EmployeeModel = Engineering.Application.WebServices.MetaDataServices.Employees.Models.Employee;

namespace Engineering.Application.WebServices.MetaDataServices.Employees.Models.GetEmployeeById;

public record GetEmployeeByIdModel
{
    [JsonProperty("data")]
    public List<EmployeeModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
