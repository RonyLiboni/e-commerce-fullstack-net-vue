using Microsoft.EntityFrameworkCore;
using Rony.Store.Domain.Contracts.Repositories.UnitOfWorks;
using Rony.Store.Domain.Exceptions;

namespace Rony.Store.Infrastructure.Database.Repositories.UnityOfWorks;
public class UnitOfWork(StoreContext dbContext) : IUnitOfWork
{
    public async Task SaveChangesAsync()
    {
        try
        {
            await dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            var errorMessage = e.InnerException!.Message ?? e.Message!;
            if (errorMessage.Contains("Duplicate"))
            {
                throw new EntityMustBeUniqueException(errorMessage);
            }

            throw e;

        }
        
    }
}
