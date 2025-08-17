using BuildingBlocks.CQRS;

namespace School.Dtos;

public record CreateStudentDto(string FirstName, string LastName, DateOnly DateOfBirth)
    : ICommand<CreateStudentResult>;
