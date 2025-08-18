using BuildingBlocks.CQRS;

namespace School.Dtos;

public record UpdateStudentDto(string FirstName, string LastName, DateTime DateOfBirth);

public record UpdateStudentWithIdDto(int Id, UpdateStudentDto Command)
    : ICommand<UpdateStudentResult>;
