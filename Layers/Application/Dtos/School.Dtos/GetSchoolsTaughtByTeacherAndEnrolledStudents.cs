using BuildingBlocks.CQRS;
using School.Entities.Extends;

namespace School.Dtos;

public record GetSchoolsTaughtByTeacherAndEnrolledStudents(int TeacherId)
    : IQuery<IEnumerable<StudentTeacherSchool>>;
