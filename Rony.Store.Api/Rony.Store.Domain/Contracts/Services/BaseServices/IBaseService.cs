namespace Rony.Store.Domain.Contracts.Services.BaseServices;
public interface IBaseService<Entity, Id>
{
    Task<Entity> FindByIdAsync(Id id);
    Task<EntityDTO> FindDTOByIdAsync<EntityDTO>(Id id);
    Task UpdateByIdAsync<EntityForm>(EntityForm form, Id id);
    Task CreateAsync<EntityForm>(EntityForm form);
}