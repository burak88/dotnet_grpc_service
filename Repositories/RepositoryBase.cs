using System;
using grpcService.Contracts;
using grpcService.Entity;
using grpcService.Models;
using Microsoft.EntityFrameworkCore;

namespace grpcService.Repositories;

public class RepositoryBase<T> : IRepository<T> where T : EntityBase
{
    private readonly AppDbContext _dbContext;
    public RepositoryBase(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<T> Create(T entity)
    {
        try
        {
            if (entity is not null)
            {
                await _dbContext.Set<T>().AddAsync(entity);
                await _dbContext.SaveChangesAsync();
            }
            return await Task.FromResult(entity);
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message},Error in RepositoryBase Create Method");
        }
    }

    public async Task<bool> Delete(string id)
    {
        try
        {
            var entity = _dbContext.Set<T>().Find(id);
            if (entity is not null)
            {
                _dbContext.Set<T>().Remove(entity);
                await _dbContext.SaveChangesAsync();
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message},Error in RepositoryBase Delete Method");
        }
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        try
        {
            return await Task.FromResult(_dbContext.Set<T>().AsEnumerable());
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message},Error in RepositoryBase GetAll Method");
        }
    }

    public async Task<T> GetById(string id)
    {
        try
        {
            var dataById = await _dbContext.Set<T>().FirstOrDefaultAsync(c => c.Id == id);
            return dataById;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error in RepositoryBase GetById Method: {ex.Message}");
        }
    }

    public async Task<T> Update(T entity)
    {
        try
        {
            var entityObject = await _dbContext.Set<T>().FindAsync(entity.Id);
            if (entityObject is not null)
            {
                _dbContext.Entry(entityObject).CurrentValues.SetValues(entity);
                await _dbContext.SaveChangesAsync();
            }
            return await Task.FromResult(entity);
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message},Error in RepositoryBase Update Method");
        }
    }
}
