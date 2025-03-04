

using BlazorAppTreino.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAppTreino.Domain.Data
{

    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : EntityDataBase
    {

        public IUnitOfWork UnitOfWork { get; }

        public IRepositoryConsult<TEntity> RepositoryConsult { get; protected set; }

        readonly DbSet<TEntity> DbSet;

        readonly ApplicationDbContext _applicationContext;
        public BaseRepository(IUnitOfWork unitOfWork,
                              IRepositoryConsult<TEntity> repositoryConsult,
                              ApplicationDbContext applicationContext)
        {

            _applicationContext = applicationContext;
            UnitOfWork = unitOfWork;
            RepositoryConsult = repositoryConsult;
            DbSet = _applicationContext.Set<TEntity>();

        }
        public void Add(TEntity entity) => DbSet.Add(entity);

        public void Dispose() => GC.SuppressFinalize(this);

        public void Remove(TEntity entity) => DbSet.Remove(entity);

        public void Update(TEntity entity) => DbSet.Update(entity);

        public async Task AddAsync(TEntity entidade) => await DbSet.AddAsync(entidade);

        public async Task AddAsync<T>(T entidade) where T : EntityDataBase => await _applicationContext.Set<T>().AddAsync(entidade);

        public void Remove<T>(T customer) where T : class
             => _applicationContext.Set<T>().Remove(customer);

    }
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : EntityDataBase
    {
        void Add(TEntity entidade);

        Task AddAsync(TEntity entidade);

        Task AddAsync<T>(T entidade) where T : EntityDataBase;

        void Update(TEntity customer);

        void Remove(TEntity customer);

        void Remove<T>(T customer) where T : class;

        IUnitOfWork UnitOfWork { get; }

        IRepositoryConsult<TEntity> RepositoryConsult { get; }
    }
}
