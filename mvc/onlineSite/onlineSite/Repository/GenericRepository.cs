using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using onlineSite.Models;

namespace onlineSite.Repository
{
    public class GenericRepository<tbl_Entity> : IRepository<tbl_Entity> where tbl_Entity : class
    {
        DbSet<tbl_Entity> _dbset;
        private DbModel _dbEntity;
        public GenericRepository(DbModel dbEntity)
        {
            _dbEntity = dbEntity;
            _dbset = _dbEntity.Set<tbl_Entity>();
        }
        public void Add(tbl_Entity db)
        {
            _dbset.Add(db);
            _dbEntity.SaveChanges();

        }

        public int GetAllRecordCount()
        {
            return _dbset.Count();
        }

        public IQueryable<tbl_Entity> GetAllRecordIQueryable()
        {
            return _dbset;
        }

        public IEnumerable<tbl_Entity> GetAllRecords()
        {
            return _dbset.ToList();
        }
       
        public tbl_Entity getFirstOrDeafault(int recordId)
        {
            return _dbset.Find(recordId);
        }

        public tbl_Entity getFirstOrDeafaultByParameter(Expression<Func<tbl_Entity, bool>> wherePredict)
        {
            return _dbset.Where(wherePredict).FirstOrDefault();
        }

        public IEnumerable<tbl_Entity> GetListParameter(Expression<Func<tbl_Entity, bool>> wherePredict)
        {
            return _dbset.Where(wherePredict).ToList();
        }

        public IEnumerable<tbl_Entity> GetProduct()
        {
            return _dbset.ToList();
        }

        public tbl_Entity GetProductById(int productId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<tbl_Entity> GetRecordToShow(int pageno, int pagesize, int currentpage, Expression<Func<tbl_Entity, bool>> wherePredict, Expression<Func<tbl_Entity, int>> orderByPredict)
        {
            if (wherePredict != null)
            {
                return _dbset.OrderBy(orderByPredict).Where(wherePredict).ToList();
            }
            else
            {
                return _dbset.OrderBy(orderByPredict).ToList();
            }
        }

        public IEnumerable<tbl_Entity> GetResultBySqlProcedure(string query, params object[] parameters)
        {
            if (parameters != null)
            {
                return _dbEntity.Database.SqlQuery<tbl_Entity>(query, parameters).ToList();
            }
            else
                return _dbEntity.Database.SqlQuery<tbl_Entity>(query).ToList();
        }

        public void InactiveAndDeleteMarkByWhereClause(Expression<Func<tbl_Entity, bool>> wherePredict, Action<tbl_Entity> ForEachpredict)
        {
            _dbset.Where(wherePredict).ToList().ForEach(ForEachpredict);
        }

        public void Remove(tbl_Entity db)
        {
            if (_dbEntity.Entry(db).State == EntityState.Detached)
            {
                _dbset.Attach(db);
            }
            _dbset.Remove(db);
        }

        public void RemoveRangeByWhereClause(Expression<Func<tbl_Entity, bool>> wherePredict)
        {
           List<tbl_Entity> db = _dbset.Where(wherePredict).ToList();
           foreach(var ent in db)
            {
                Remove(ent);
            }
        }

        public void RemoveWhereClause(Expression<Func<tbl_Entity, bool>> wherePredict)
        {
            tbl_Entity db = _dbset.Where(wherePredict).FirstOrDefault();
            Remove(db);
        }

        public void Update(tbl_Entity db)
        {
            _dbset.Attach(db);
            _dbEntity.Entry(db).State = EntityState.Modified;
        }

        public void UpdateWhereClause(Expression<Func<tbl_Entity, bool>> wherePredict, Action<tbl_Entity> ForEachpredict)
        {
            _dbset.Where(wherePredict).ToList().ForEach(ForEachpredict);
        }
    }
}