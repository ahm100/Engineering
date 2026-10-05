using Engineering.Application.Services.MachineTypes.Models.MachineTypeModels;

namespace Engineering.Application.Services.MachineTypes.Models.GetsActiveMachineType;

public record GetsActiveMachineTypeResponse(
    List<GetsActiveMachineTypeModel> Data,
    int RowCount
    );
