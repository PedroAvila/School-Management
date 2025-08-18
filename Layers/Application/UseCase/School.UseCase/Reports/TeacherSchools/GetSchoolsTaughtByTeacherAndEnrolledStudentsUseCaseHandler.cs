using System.Net;
using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using School.Dtos;
using School.Entities.Extends;
using School.Ports;

namespace School.UseCase.Reports.TeacherSchools;

public class GetSchoolsTaughtByTeacherAndEnrolledStudentsUseCaseHandler(
    ITeacherRepository _teacherRepository
) : IQueryHandler<GetSchoolsTaughtByTeacherAndEnrolledStudents, IEnumerable<StudentTeacherSchool>>
{
    public async Task<IEnumerable<StudentTeacherSchool>> Handle(
        GetSchoolsTaughtByTeacherAndEnrolledStudents query,
        CancellationToken cancellationToken
    )
    {
        var existingTeacher = await _teacherRepository.ExistAsync(x => x.Id == query.TeacherId);
        if (!existingTeacher)
            throw new BusinessException(
                $"Teacher with ID '{query.TeacherId}' not found.",
                HttpStatusCode.NotFound
            );
        return await _teacherRepository.GetTeacherSchoolsWithStudentsAsync(query.TeacherId);
    }
}
