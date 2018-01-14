using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.ORM.Core.Operator
{
    /// <summary>
    /// 复杂自由的SQL语句执行接口
    /// </summary>
    public interface IDBFreeSQL
    {
        Task<int> ExecuteNonQueryAsync(string sql);

        Task<int> ExecuteNonQueryAsync(string sql, params DbParameter[] parms);

        Task<int> ExecuteNonQueryAsync(DbCommand cmd);

        Task<DbDataReader> ExecuteReaderAsync(string sql);

        Task<DbDataReader> ExecuteReaderAsync(string sql, params DbParameter[] parms);

        Task<DbDataReader> ExecuteReaderAsync(DbCommand cmd);

        Task<object> ExecuteScalarAsync(string sql);

        Task<object> ExecuteScalarAsync(string sql, params DbParameter[] parms);

        Task<object> ExecuteScalarAsync(DbCommand cmd);

        Task<TEntity> ExecuteAsync<TEntity>(string sql);

        Task<TEntity> ExecuteAsync<TEntity>(string sql, params DbParameter[] parms);

        Task<TEntity> ExecuteAsync<TEntity>(DbCommand cmd);

        Task<IList<TEntity>> ExecuteListAsync<TEntity>(string sql);

        Task<IList<TEntity>> ExecuteListAsync<TEntity>(string sql, params DbParameter[] parms);

        Task<IList<TEntity>> ExecuteListAsync<TEntity>(DbCommand cmd);
    }
}
