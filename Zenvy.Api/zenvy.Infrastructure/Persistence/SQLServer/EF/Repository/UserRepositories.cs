using System;
using System.Collections.Generic;
using System.Text;
using zenvy.application.Interfaces.Repositories;
using zenvy.domain.Entities;

namespace zenvy.infrastructure.Persistence.SqlServer.EF.Repository
{
    internal class EfUserRepositories : IUserRepository
    {
        Task IUserRepository.AddAsync(User user)
        {
            throw new NotImplementedException();
        }

        Task IUserRepository.DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<List<User>> IUserRepository.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<User?> IUserRepository.GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        Task<User?> IUserRepository.GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task IUserRepository.UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
