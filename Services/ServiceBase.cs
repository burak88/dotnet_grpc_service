using System;
using grpcService.Contracts;
using grpcService.Entity;

namespace grpcService.Services;

public class ServiceBase<T> : IService<T> where T : EntityBase
{
    private readonly IRepository<T> _repository;
    public ServiceBase(IRepository<T> repository)
    {
        _repository = repository;
    }
    public async Task<T> Create(T @object)
    {
       return await _repository.Create(@object);
    }

    public async Task<bool> Delete(string id)
    {
        return await _repository.Delete(id);
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<T> GetById(string id)
    {
        return await _repository.GetById(id);
    }

    public async Task<T> Update(T @object)
    {
        return await _repository.Update(@object);
    }
}
