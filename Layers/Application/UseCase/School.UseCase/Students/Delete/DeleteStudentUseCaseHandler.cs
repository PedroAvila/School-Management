using BuildingBlocks.CQRS;
using School.Dtos;
using School.Ports;

namespace School.UseCase.Students.Delete;

public class DeleteStudentUseCaseHandler(IStudentRepository _studentRepository)
    : ICommandHandler<DeleteStudentDto, DeleteStudentResult>
{
    public async Task<DeleteStudentResult> Handle(
        DeleteStudentDto command,
        CancellationToken cancellationToken
    )
    {
        var result = await _studentRepository.DeleteAsync(command.Id);
        return new DeleteStudentResult(result);
    }
}
