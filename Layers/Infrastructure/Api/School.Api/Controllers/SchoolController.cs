using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Dtos;

namespace School.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSchoolDto dto)
        {
            var result = await _mediator.Send(dto);
            return Created(string.Empty, result);
        }
    }
}
