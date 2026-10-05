namespace Engineering.Domain.Entities.Contracts.Enums;

public enum ContractTypeKind
{
    [Description(GlobalCmts.ContractTypeKindProcurement)]
    Procurement = 1,

    [Description(GlobalCmts.ContractTypeKindEngineering)]
    Engineering = 2,

    [Description(GlobalCmts.ContractTypeKindConstruction)]
    Construction = 3,

    [Description(GlobalCmts.ContractTypeKindServices)]
    Services = 4
}