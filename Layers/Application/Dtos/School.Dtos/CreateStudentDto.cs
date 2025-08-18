using BuildingBlocks.CQRS;

namespace School.Dtos;

public record CreateStudentDto(string FirstName, string LastName, DateTime DateOfBirth)
    : ICommand<CreateStudentResult>;
