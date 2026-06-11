using System;
using System.Collections.Generic;
using System.Text;

namespace zenvy.application.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}
