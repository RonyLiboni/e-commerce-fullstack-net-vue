namespace Rony.Store.Domain.Contracts.Repositories.UnitOfWorks;
public interface IUnitOfWork
{
    Task SaveChangesAsync();
}