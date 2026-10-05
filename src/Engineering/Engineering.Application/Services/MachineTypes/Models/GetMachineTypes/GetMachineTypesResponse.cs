using Engineering.Application.Services.MachineTypes.Models.MachineTypeModels;

namespace Engineering.Application.Services.MachineTypes.Models.GetMachineTypes;

public record GetMachineTypesResponse(
    List<GetMachineTypesModel> Data,
    int RowCount
    );
