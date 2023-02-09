﻿using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess
{
    internal class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        internal MyAppDbContext context;
        internal DbSet<TEntity> dbSet;

        public Repository(MyAppDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async virtual Task<TEntity?> GetByID(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public async virtual Task Insert(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async virtual Task Delete(object id)
        {
            TEntity entityToDelete = await dbSet.FindAsync(id);
            await Delete(entityToDelete);
        }

        public virtual Task Delete(TEntity entityToDelete)
        {
            return Task.Run(() =>
            {
                if (context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    dbSet.Attach(entityToDelete);
                }
                dbSet.Remove(entityToDelete);
            });
        }

        public virtual Task Update(TEntity entityToUpdate)
        {
            return Task.Run(() =>
            {
                dbSet.Attach(entityToUpdate);
                context.Entry(entityToUpdate).State = EntityState.Modified;
            });
        }

        public async Task<TEntity?> GetItemBySpec(ISpecification<TEntity> specification)
        {
            // apply specification
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetListBySpec(ISpecification<TEntity> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
        {
            var evaluator = new SpecificationEvaluator();
            return evaluator.GetQuery(dbSet, specification);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await dbSet.ToListAsync();
        }
    }
}
