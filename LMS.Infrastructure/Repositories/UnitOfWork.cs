﻿using Domain.Contracts;
using LMS.Infrastructure.Data;

namespace LMS.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly LmsContext context;

    public UnitOfWork(LmsContext context)
    {
        this.context = context;
    }

    public ICourseRepo CourseRepo { get; }
    public IModuleRepo ModuleRepo { get; }

    public async Task CompleteASync()
    {
        await context.SaveChangesAsync();
    }
}
