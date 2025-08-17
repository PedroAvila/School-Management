using BuildingBlocks.CQRS;

namespace School.Dtos;

public record CreateTeacherDto(int SchoolId, string FirstName, string LastName)
    : ICommand<CreateTeacherResult>;
