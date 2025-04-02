﻿namespace Weather.Application.Abstraction.Data;
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}