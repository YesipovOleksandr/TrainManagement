using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache _memoryCache;
        private readonly List<string> _cacheKeys = new List<string>();

        public TrainComponentsController(ITrainComponentService trainComponentService, IMapper mapper, IMemoryCache memoryCache)
        {
            _trainComponentService = trainComponentService;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<ActionResult<TrainComponentList>> GetTrainComponents([FromQuery] int number, [FromQuery] int page, string? search = null)
        {
            string cacheKey = $"TrainComponents_{page}_{number}_{search}";

            if (!_memoryCache.TryGetValue(cacheKey, out TrainComponentList components))
            {
                components = await _trainComponentService.GetAll(page, number, search);

                if (components != null)
                {
                    _memoryCache.Set(cacheKey, components, TimeSpan.FromMinutes(10));
                    _cacheKeys.Add(cacheKey); 
                }
            }

            return Ok(components);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TrainComponent>> GetTrainComponent(long id)
        {
            string cacheKey = $"TrainComponent_{id}";

            if (_memoryCache.TryGetValue(cacheKey, out TrainComponent cachedComponent))
            {
                return Ok(cachedComponent);
            }

            var component = await _trainComponentService.GetById(id);

            if (component == null)
            {
                return NotFound();
            }

            _memoryCache.Set(cacheKey, component, TimeSpan.FromMinutes(20));
            _cacheKeys.Add(cacheKey);

            return Ok(component);
        }

        [HttpPost]
        public async Task<ActionResult<TrainComponent>> CreateTrainComponent([FromBody] CreateTrainComponentViewModel model)
        {
            var isUnique = await _trainComponentService.IsUniqueNumberExists(model.UniqueNumber);

            if (isUnique)
            {
                return BadRequest(new { message = "Unique number must be unique." });
            }

            var createdComponent = await _trainComponentService.Create(_mapper.Map<TrainComponent>(model));

            ClearCache();

            return CreatedAtAction("GetTrainComponent", new { id = createdComponent.Id }, createdComponent);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainComponent(long id)
        {
            await _trainComponentService.DeleteById(id);
            ClearCache();
            return NoContent();
        }

        [HttpPatch("{id}/quantity")]
        public async Task<IActionResult> PatchQuantity(long id, [FromBody] int quantity)
        {
            try
            {
                await _trainComponentService.UpdateQuantity(id, quantity);
                ClearCache();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request.", details = ex.Message });
            }

            return NoContent();
        }

        private void ClearCache()
        {
            foreach (var key in _cacheKeys)
            {
                _memoryCache.Remove(key);
            }

            _cacheKeys.Clear();
        }
    }
}
