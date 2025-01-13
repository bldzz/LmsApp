using AutoMapper;
using Domain.Contracts;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace LMS.Services;
public abstract class ServiceBase<TEntity, TDto, TCreationDto>
   where TEntity : class
   where TDto : class
   where TCreationDto : class
{
    protected readonly IMapper _mapper;
    protected readonly IUnitOfWork _uow;

    protected ServiceBase(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _uow = unitOfWork;
    }

    protected abstract Task<IEnumerable<TEntity>> GetAllEntitiesAsync();
    protected abstract Task<TEntity> GetEntityByIdAsync(int id);
    protected abstract void CreateEntity(TEntity entity);
    protected abstract void UpdateEntity(TEntity entity);
    protected abstract void DeleteEntity(TEntity entity);
    protected abstract Task<TEntity> FindEntityByIdAsync(int id);
    protected abstract Task<bool> ValidateEntityExistsAsync(int id);

    public virtual async Task<IEnumerable<TDto>> GetAllAsync()
    {
        var entities = await GetAllEntitiesAsync();
        return _mapper.Map<IEnumerable<TDto>>(entities);
    }

    public virtual async Task<TDto> GetByIdAsync(int id)
    {
        var entity = await GetEntityByIdAsync(id);
        return _mapper.Map<TDto>(entity);
    }

    public virtual async Task<TDto> CreateAsync(TCreationDto creationDto)
    {
        var entity = _mapper.Map<TEntity>(creationDto);
        CreateEntity(entity);
        await _uow.CompleteASync();
        var createdEntity = await FindEntityByIdAsync(EF.Property<int>(entity, "Id"));
        return _mapper.Map<TDto>(createdEntity);
    }

    public virtual async Task<TDto> UpdateAsync(int id, TDto dto)
    {
        if (id != (int)typeof(TDto).GetProperty("Id").GetValue(dto))
        {
            throw new InvalidDataException();
        }

        var existingEntity = await GetEntityByIdAsync(id);
        if (existingEntity == null) throw new InvalidDataException();

        _mapper.Map(dto, existingEntity);
        UpdateEntity(existingEntity);
        await _uow.CompleteASync();

        var updatedEntity = await GetEntityByIdAsync(id);
        return _mapper.Map<TDto>(updatedEntity);
    }

    public virtual async Task<TDto> DeleteAsync(int id)
    {
        var entityToDelete = await GetEntityByIdAsync(id);
        if (entityToDelete == null) throw new InvalidDataException();

        DeleteEntity(entityToDelete);
        await _uow.CompleteASync();

        if (await ValidateEntityExistsAsync(id))
            throw new InvalidOperationException();

        return _mapper.Map<TDto>(entityToDelete);
    }

    public virtual async Task<TDto> PatchAsync(int id, JsonPatchDocument<TDto> patchDoc)
    {
        var existingEntity = await GetEntityByIdAsync(id);
        if (existingEntity == null) throw new InvalidDataException();

        var dto = _mapper.Map<TDto>(existingEntity);
        patchDoc.ApplyTo(dto);

        _mapper.Map(dto, existingEntity);
        UpdateEntity(existingEntity);
        await _uow.CompleteASync();

        var updatedEntity = await GetEntityByIdAsync(id);
        return _mapper.Map<TDto>(updatedEntity);
    }
}
