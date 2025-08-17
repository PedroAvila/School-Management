namespace School.Dtos;

public record CreateStudentResult(
    int Id,
    int Code,
    string FirstName,
    string LastName,
    DateOnly DateOfBirth
);
