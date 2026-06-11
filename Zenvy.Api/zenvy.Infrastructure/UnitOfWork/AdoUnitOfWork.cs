using System;
using System.Collections.Generic;
using System.Text;
using zenvy.application.Interfaces;

namespace zenvy.infrastructure.UnitOfWork
{
    internal class AdoUnitOfWork: IUnitOfWork
    {
        public Task<int> SaveChangesAsync()
        {
            return Task.FromResult(1);
        }
    }
}
