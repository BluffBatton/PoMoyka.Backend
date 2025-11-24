using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoMoyka.Backend.Application.Services.Statement;
using PoMoyka.Backend.Contracts.DTOs.CreateDTOs;
using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;

namespace PoMoyka.Backend.API.Controllers
{
    [Authorize]
    public class StatementController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateStatement(StatementCreateDto dto)
        {
            var command = new CreateStatementCommand(dto);
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<StatementDto>>> GetAllStatements()
        {
            var query = new GetAllStatementsQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StatementDto>> GetStatementById(Guid id)
        {
            var query = new GetStatementByIdQuery(id);
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
