using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TrainManagement.API.ViewModels;
using TrainManagement.Common.Abstract.Services;
using TrainManagement.Common.Models;

namespace TrainManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainComponentsController : ControllerBase
    {
        private readonly ITrainComponentService _trainComponentService;
        private readonly IMapper _mapper;

        public TrainComponentsController(ITrainComponentService trainComponentService, IMapper mapper)
        {
            _trainComponentService = trainComponentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainComponent>>> GetTrainComponents()
        {
            var components = await _trainComponentService.GetAll();
            return Ok(components);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TrainComponent>> GetTrainComponent(long id)
        {
            var component = await _trainComponentService.GetById(id);
            if (component == null)
            {
                return NotFound();
            }
            return Ok(component);
        }

        [HttpPost]
        public async Task<ActionResult<TrainComponent>> PostTrainComponent([FromBody] CreateTrainComponentViewModel model)
        {
            var createdComponent = await _trainComponentService.Create(_mapper.Map<TrainComponent>(model));
            return CreatedAtAction("GetTrainComponent", new { id = createdComponent.Id }, createdComponent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainComponent(long id, int quantity)
        {
            try
            {
                await _trainComponentService.UpdateQuantity(id, quantity);
            }
            catch (ArgumentException)
            {
                return BadRequest("Component ID mismatch.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainComponent(long id)
        {
            await _trainComponentService.DeleteById(id);
            return NoContent();
        }

        [HttpPatch("{id}/quantity")]
        public async Task<IActionResult> PatchQuantity(long id, [FromBody] int quantity)
        {
            try
            {
                await _trainComponentService.UpdateQuantity(id, quantity);
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Quantity assignment is not allowed.");
            }

            return NoContent();
        }
    }
}
