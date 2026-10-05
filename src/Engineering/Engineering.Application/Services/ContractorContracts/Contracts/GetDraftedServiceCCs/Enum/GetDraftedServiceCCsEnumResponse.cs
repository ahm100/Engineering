namespace Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedServiceCCs.Enum;

public record GetDraftedServiceCCsEnumResponse(
    List<EnumObject> Contract,
    List<EnumObject> Daily
);