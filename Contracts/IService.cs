using System;

namespace grpcService.Contracts;

public interface IService<T>
{
    public Task<IEnumerable<T>> GetAll();
    public Task<T> GetById(string id);
    public Task<T> Create(T @object);
    public Task<T> Update(T @object);
    public Task<bool> Delete(string id);
}
