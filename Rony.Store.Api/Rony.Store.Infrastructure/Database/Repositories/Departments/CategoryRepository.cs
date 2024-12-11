﻿using Rony.Store.Domain.Contracts.Repositories.Departments;
using Rony.Store.Domain.Entities.Departments;
using Rony.Store.Infrastructure.Database.Repositories.BaseRepositories;

namespace Rony.Store.Infrastructure.Database.Repositories.Departments;
public class CategoryRepository(StoreContext context) : BaseRepository<Category, int>(context), ICategoryRepository
{
    protected override string EntityName() => "Category";
}