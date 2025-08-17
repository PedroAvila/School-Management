using BuildingBlocks.CQRS;

namespace School.Dtos;

public record UpdateTeacherDto(int SchoolId, string FirstName, string LastName);

public record UpdateTeacherWithIdDto(int Id, UpdateTeacherDto Command)
    : ICommand<UpdateTeacherResult>;
