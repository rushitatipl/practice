using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;

namespace onlineSite.Repository
{
    public interface IRepository<tbl_Entity>where tbl_Entity:class
    {
        IEnumerable<tbl_Entity> GetAllRecords();
        IEnumerable<tbl_Entity> GetProduct();
        tbl_Entity GetProductById(int productId);
        IQueryable<tbl_Entity> GetAllRecordIQueryable();
        int GetAllRecordCount();
        void Add(tbl_Entity db);
        void Update(tbl_Entity db);
        void UpdateWhereClause(Expression<Func<tbl_Entity, bool>> wherePredict, Action<tbl_Entity> ForEachpredict);
        tbl_Entity getFirstOrDeafault(int recordId);

        void Remove(tbl_Entity db);
        void RemoveWhereClause(Expression<Func<tbl_Entity, bool>> wherePredict);
        void RemoveRangeByWhereClause(Expression<Func<tbl_Entity, bool>> wherePredict);
        void InactiveAndDeleteMarkByWhereClause(Expression<Func<tbl_Entity, bool>> wherePredict, Action<tbl_Entity> ForEachpredict);
        tbl_Entity getFirstOrDeafaultByParameter(Expression<Func<tbl_Entity, bool>> wherePredict);
        IEnumerable<tbl_Entity> GetListParameter(Expression<Func<tbl_Entity, bool>> wherePredict);
        IEnumerable<tbl_Entity> GetResultBySqlProcedure(string query,params object[] parameters);
        IEnumerable<tbl_Entity> GetRecordToShow(int pageno,int pagesize,int currentpage, Expression<Func<tbl_Entity, bool>> wherePredict, Expression<Func<tbl_Entity, int>> orderByPredict);

    }
}