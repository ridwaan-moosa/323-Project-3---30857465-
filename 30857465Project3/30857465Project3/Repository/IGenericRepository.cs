using _30857465Project3.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace _30857465Project3.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(Guid id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        bool ZoneExists(Guid zoneId);
        void SaveChangesAsync();
        public void Update(T entity);
        public T FindAsync(Guid id);
        public bool CategoryExists(Guid id);
        public bool DeviceExists(Guid id);
        public DbSet<Category> Category();
        public DbSet<Zone> Zone();


    }
}
