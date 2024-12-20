using Microsoft.EntityFrameworkCore;
using Rony.Store.Domain.Contracts.Repositories.UnitOfWorks;
using Rony.Store.Domain.Exceptions;
using System.Text.RegularExpressions;

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
            if (errorMessage.Contains("duplicate", StringComparison.CurrentCultureIgnoreCase))
            {
                var uniqueConstraintName = Regex.Match(errorMessage, @"IX_\w+").Value.Split("_").LastOrDefault() ?? "";
                throw new EntityMustBeUniqueException(string.IsNullOrEmpty(uniqueConstraintName) ? e.Message : $"Field with duplicated value: {uniqueConstraintName}");
            }
            if (errorMessage.Contains("foreign key", StringComparison.CurrentCultureIgnoreCase))
            {
                throw new ForeignKeyRelatedExceptionn(e.Message);
            }
            throw;
        }
    }
}
