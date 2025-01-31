namespace Rony.Store.Domain.Contracts.Services.BaseServices;
public interface IBaseCRUDService<Entity, Id> : IBaseService<Entity, Id>
{
    Task RemoveByIdAsync(Id id);
}
