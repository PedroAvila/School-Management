using BuildingBlocks.CQRS;
using School.Entities.Extends;

namespace School.Dtos;

public record GetStudentsEnrolledPerTeacher(int TeacherId)
    : IQuery<IEnumerable<StudentTeacherSchool>>;
