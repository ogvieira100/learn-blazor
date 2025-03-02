using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAppTreino.Domain.Data
{

    public class UnitOfWork : IUnitOfWork
    {
        readonly DbContext _dbContext;

        public UnitOfWork(DbContext dbContext)
        {

            _dbContext = dbContext;

        }
        public async Task<bool> CommitAsync() => await _dbContext.SaveChangesAsync() > 0;

        public void Dispose() => GC.SuppressFinalize(this);
    }
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> CommitAsync();
    }
}
