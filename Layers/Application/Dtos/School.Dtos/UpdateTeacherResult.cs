namespace School.Dtos;

public record UpdateTeacherResult(
    int Id,
    int SchoolId,
    int Code,
    string FirstName,
    string LastName
);
