using BBGymManagement.Models.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace BBGymManagement.Services
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected EFDbContext context;

        public Repository()
        {
            context = new EFDbContext();
        }

        public Repository(EFDbContext dbContext)
        {
            context = dbContext;
        }
        
        public DbSet<T> Table()
        {
            return context.Set<T>();
        }
        
        public DbSet<TEntity> Table<TEntity>() where TEntity : class
        {
            return context.Set<TEntity>();
        }
        public void Add(T model)
        {
            Table<T>().Add(model);
            context.SaveChanges();
        }

        public List<T> Get(Expression<Func<T, bool>> metot)
        {
            return Table<T>().Where(metot).ToList();    
        }

        public List<T> GetAll()
        {
            return Table<T>().ToList();
        }

        public T GetById(int id)
        {
            return GetById<T>(id);
        }

        public A GetById<A>(int id) where A : class
        {
            return GetSingle<A>(t => typeof(A).GetProperty("Id").GetValue(t).ToString() == id.ToString());
        }
        public TEntity GetSingle<TEntity>(Func<TEntity, bool> metot) where TEntity : class
        {
            return Table<TEntity>().FirstOrDefault(metot);
        }
    
        public void Remove(int id)
        {
            Table<T>().Remove(GetById<T>(id));
            context.SaveChanges();
        }
        
        public void Update(T model, int id)
        {
            Update<T>(model, id);
            context.SaveChanges();
        }

        public void Update<A>(A model, int id) where A : class
        {
            A guncellenecekNesne = GetById<A>(id);
            var tumPropertyler = typeof(A).GetProperties();
            foreach (var property in tumPropertyler)
                if (property.Name != "Id")
                    property.SetValue(guncellenecekNesne, property.GetValue(model));
        }
    }
}