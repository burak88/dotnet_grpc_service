using System;

namespace grpcService.Contracts;

public interface IRepository<T> 
{
    public Task<IEnumerable<T>> GetAll();
    public Task<T> GetById(string id);
    public Task<T> Create(T entity);
    public Task<T> Update(T entity);
    public Task<bool> Delete(string id);
}
