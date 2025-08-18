using BuildingBlocks.CQRS;

namespace School.Dtos;

public record CreateRegistrationDto(int StudentId, int SchoolId)
    : ICommand<CreateRegistrationResult>;
