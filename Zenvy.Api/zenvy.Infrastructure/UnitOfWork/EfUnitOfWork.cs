using System;
using System.Collections.Generic;
using System.Text;
using zenvy.application.Interfaces;

namespace zenvy.infrastructure.UnitOfWork
{
    internal class EfUnitOfWork(AppDbContext db) : IUnitOfWork
    {
        public async Task<int> SaveChangesAsync()
        {
            return await db.SaveChangesAsync();
        }
    }
}
