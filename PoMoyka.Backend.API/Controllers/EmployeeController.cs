using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoMoyka.Backend.Application.Common.Mappings.CreateDTOMappings;
using PoMoyka.Backend.Application.Services.Employee;
using PoMoyka.Backend.Application.Services.Employee.Commands;
using PoMoyka.Backend.Application.Services.Employee.Queries;
using PoMoyka.Backend.Contracts.DTOs.UpdateDTOs;

namespace PoMoyka.Backend.API.Controllers
{
    [Authorize]
    public class EmployeeController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllEmpoyees()
        {
            var query = new GetAllEmployeesQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            var query = new GetEmployeeByIdQuery(id);
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeCreateDto dto)
        {
            var command = new CreateEmployeeCommand(dto);
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] EmployeeUpdateDto dto)
        {
            var command = new UpdateEmployeeCommand(id, dto);
            await Mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var command = new DeleteEmployeeCommand(id);
            await Mediator.Send(command);
            return Ok();
        }
    }
}
