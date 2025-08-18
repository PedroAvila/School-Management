namespace School.Dtos;

public record CreateRegistrationResult(
    int Id,
    int Code,
    int StudentId,
    int SchoolId,
    DateTime RegistrationDate
);
