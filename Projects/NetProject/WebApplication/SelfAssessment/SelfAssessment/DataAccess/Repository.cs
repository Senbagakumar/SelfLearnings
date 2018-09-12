using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace SelfAssessment.DataAccess
{   


    public class Repository<TObject> : IRepository<TObject>  where TObject : class
    {
        protected AssessmentContext _context= null;
   
        public Repository()
        {
            _context = new AssessmentContext();
        }

        public Repository(AssessmentContext context)
        {
            _context = context;            
        }

        public AssessmentContext AssessmentContext => _context;

        protected DbSet<TObject> DbSet
        {
            get
            {
                return _context.Set<TObject>();
            }
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }

        public virtual IQueryable<TObject> All()
        {
            return DbSet.AsQueryable();
        }

        public virtual IQueryable<TObject> Filter(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.Where(predicate).AsQueryable<TObject>();
        }

        public virtual IQueryable<TObject> Filter<key>(Expression<Func<TObject, bool>> filter, out int total, int index = 0, int size = 50)
        {
            int skipCount = index * size;
            var _resetSet = filter != null ? DbSet.Where(filter).AsQueryable() :
                DbSet.AsQueryable();
            _resetSet = skipCount == 0 ? _resetSet.Take(size) :
                _resetSet.Skip(skipCount).Take(size);
            total = _resetSet.Count();
            return _resetSet.AsQueryable();
        }

        public bool Contains(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.Count(predicate) > 0;
        }

        public virtual TObject Find(params object[] keys)
        {
            return DbSet.Find(keys);
        }

        public virtual TObject Find(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public virtual TObject Create(TObject TObject)
        {
            var newEntry = DbSet.Add(TObject);
            return newEntry;
        }

        public virtual int Count
        {
            get
            {
                return DbSet.Count();
            }
        }

        public void Delete(TObject TObject)
        {
            DbSet.Remove(TObject);
        }

        public void DeleteRange(List<TObject> listTObject)
        {
            DbSet.RemoveRange(listTObject);
        }

        public virtual int Update(TObject TObject)
        {
            var entry = _context.Entry(TObject);
            DbSet.Attach(TObject);
            entry.State = EntityState.Modified;
            return 0;
        }

        public virtual int Delete(Expression<Func<TObject, bool>> predicate)
        {
            var objects = Filter(predicate);
            DbSet.RemoveRange(objects);         
            return 0;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}