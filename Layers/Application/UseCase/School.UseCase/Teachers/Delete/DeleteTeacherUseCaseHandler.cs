using BuildingBlocks.CQRS;
using School.Dtos;
using School.Ports;

namespace School.UseCase.Teachers.Delete;

public class DeleteTeacherUseCaseHandler(ITeacherRepository _teacherRepository)
    : ICommandHandler<DeleteTeacherDto, DeleteTeacherResult>
{
    public async Task<DeleteTeacherResult> Handle(
        DeleteTeacherDto command,
        CancellationToken cancellationToken
    )
    {
        var result = await _teacherRepository.DeleteAsync(command.Id);
        return new DeleteTeacherResult(result);
    }
}
