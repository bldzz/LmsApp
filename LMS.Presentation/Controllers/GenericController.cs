using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace LMS.Presentation.Controllers;

//[ApiController]
//[Route("api/[controller]")]
//public class GenericController<TEntity> : ControllerBase where TEntity : class
//{
//    private readonly IServiceManager _serviceManager;

//    public GenericController(IServiceManager serviceManager)
//    {
//        _serviceManager = serviceManager;
//    }

//    [HttpGet]
//    public async Task<ActionResult<IEnumerable<TEntity>>> GetAll()
//    {
//        var entities = await _serviceManager.GetService<TEntity>().GetAllAsync();
//        return Ok(entities);
//    }

//    [HttpGet("{id}")]
//    public async Task<ActionResult<TEntity>> GetById(int id)
//    {
//        var entity = await _serviceManager.GetService<TEntity>().GetByIdAsync(id);
//        return entity == null ? NotFound() : Ok(entity);
//    }

//    [HttpPost]
//    public async Task<ActionResult> Create([FromBody] TEntity entity)
//    {
//        await _serviceManager.GetService<TEntity>().AddAsync(entity);
//        return CreatedAtAction(nameof(GetById), new { id = entity.GetHashCode() }, entity);
//    }

//    [HttpDelete("{id}")]
//    public async Task<ActionResult> Delete(int id)
//    {
//        var service = _serviceManager.GetService<TEntity>();
//        var entity = await service.GetByIdAsync(id);
//        if (entity == null)
//            return NotFound();

//        await service.RemoveAsync(entity);
//        return NoContent();
//    }
//}

