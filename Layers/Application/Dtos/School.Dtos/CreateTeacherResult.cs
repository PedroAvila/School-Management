namespace School.Dtos;

public record CreateTeacherResult(
    int Id,
    int SchoolId,
    int Code,
    string FirstName,
    string LastName
);
