using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Dtos;

namespace School.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStudentDto dto)
        {
            var result = await _mediator.Send(dto);
            return Created(string.Empty, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateStudentDto dto)
        {
            var command = new UpdateStudentWithIdDto(id, dto);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteStudentDto(id));
            return Ok(result);
        }

        [HttpPost("AssignStudentToTeacher")]
        public async Task<IActionResult> AssignStudentToTeacher(
            [FromBody] AssignStudentToTeacherDto dto
        )
        {
            var result = await _mediator.Send(dto);
            return Created(string.Empty, result);
        }
    }
}
