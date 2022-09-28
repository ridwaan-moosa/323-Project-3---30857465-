using _30857465Project3.Data;
using _30857465Project3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace _30857465Project3.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ConnectedOfficeContext _context;
        public GenericRepository(ConnectedOfficeContext context)
        {
            _context = context;
        }
        public  void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            
        }
        public void SaveChangesAsync()
        {
            _context.SaveChangesAsync();
        }

        public void Update(T entity)
        { _context.Set<T>().Update(entity);
        }
        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }
        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }
        public T FindAsync(Guid id)
        {
            return _context.Set<T>().Find(id);
        }
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        
        public T GetById(Guid id)
        {
            return _context.Set<T>().Find(id);
        }
        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }
        public bool ZoneExists(Guid id)
        {
            return _context.Zone.Any(e => e.ZoneId == id);
        }
        public bool CategoryExists(Guid id)
        {
            return _context.Category.Any(e => e.CategoryId == id);
        }
        public bool DeviceExists(Guid id)
        {
            return _context.Device.Any(e => e.DeviceId == id);
        }
        public DbSet<Category> Category()
        {

            return _context.Category;
        }
        public DbSet<Zone> Zone()
        {

            return _context.Zone;
        }
        

    }

}

