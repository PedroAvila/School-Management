using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Dtos;

namespace School.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepostsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("student/{teacherId}")]
        public async Task<IActionResult> GetStudentsEnrolledPerTeacher(int teacherId)
        {
            var result = await _mediator.Send(new GetStudentsEnrolledPerTeacher(teacherId));
            return Ok(result);
        }

        [HttpGet("schools-with-students/{teacherId}")]
        public async Task<IActionResult> GetSchoolsTaughtByTeacherAndEnrolledStudents(int teacherId)
        {
            var result = await _mediator.Send(
                new GetSchoolsTaughtByTeacherAndEnrolledStudents(teacherId)
            );
            return Ok(result);
        }
    }
}
