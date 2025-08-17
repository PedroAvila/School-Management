namespace School.Dtos;

public record UpdateStudentResult(
    int Id,
    int Code,
    string FirstName,
    string LastName,
    DateOnly DateOfBirth
);
