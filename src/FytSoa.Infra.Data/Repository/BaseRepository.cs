using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Net;
using SqlSugar;
using FytSoa.Domain.Repository.Interfaces;
using FytSoa.Infra.Common;
using FytSoa.Infra.Data.Context;

namespace FytSoa.Infra.Data.Repository
{
    public class BaseRepository<T>: SugarDbContext, IBaseRepository<T> where T : class, new()
    {

        public BaseRepository()
        {
        }
        #region 添加操作
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="parm">T</param>
        /// <returns></returns>
        public async Task<int> AddAsync(T parm, bool Async = true)
        {
            return Async ? await Db.Insertable<T>(parm).ExecuteCommandAsync() : Db.Insertable<T>(parm).ExecuteCommand();
        }

        /// <summary>
        /// 批量添加数据
        /// </summary>
        /// <param name="parm">List<T></param>
        /// <returns></returns>
        public async Task<int> AddListAsync(List<T> parm, bool Async = true)
        {
            return Async ? await Db.Insertable<T>(parm).ExecuteCommandAsync() : Db.Insertable<T>(parm).ExecuteCommand();
        }
        #endregion

        #region 查询操作
        /// <summary>
        /// 获得一条数据
        /// </summary>
        /// <param name="where">Expression<Func<T, bool>></param>
        /// <returns></returns>
        public async Task<T> GetModelAsync(Expression<Func<T, bool>> where, bool Async = true)
        {
            return Async ? await Db.Queryable<T>().Where(where).FirstAsync() ?? new T() { }
                : Db.Queryable<T>().Where(where).First() ?? new T() { };
        }

        /// <summary>
        /// 获得一条数据
        /// </summary>
        /// <param name="parm">string</param>
        /// <returns></returns>
        public async Task<T> GetModelAsync(string parm, bool Async = true)
        {
            return Async ? await Db.Queryable<T>().Where(parm).FirstAsync() ?? new T() { }
                : Db.Queryable<T>().Where(parm).First() ?? new T() { };
        }


        /// <summary>
        /// 根据条件查询列表
        /// </summary>
        /// <param name="where">拉姆达条件</param>
        /// <param name="order">拉姆达排序</param>
        /// <param name="orderEnum">枚举，1=desc 2=asc</param>
        /// <param name="Async">是否同步</param>
        /// <returns></returns>
        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> where,
            Expression<Func<T, object>> order, int orderEnum, bool Async = true)
        {
            var query = Db.Queryable<T>()
                        .Where(where)
                        .OrderByIF(orderEnum == 1, order, OrderByType.Desc)
                        .OrderByIF(orderEnum == 2, order, OrderByType.Asc);
            return Async ? await query.ToListAsync() : query.ToList();
        }

        /// <summary>
        /// 获得列表，不需要任何条件
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> GetListAsync(bool Async = true)
        {
            return Async ? await Db.Queryable<T>().ToListAsync() : Db.Queryable<T>().ToList();
        }
        #endregion

        #region 修改操作
        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <param name="parm">T</param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(T parm, bool Async = true)
        {
            return Async ? await Db.Updateable<T>(parm).ExecuteCommandAsync() : Db.Updateable<T>(parm).ExecuteCommand();
        }

        /// <summary>
        /// 修改一条数据，增加忽略项
        /// </summary>
        /// <param name="model">T</param>
        /// <param name="ignore">忽略列，如new {it.id,it.name}</param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(T model,
            Expression<Func<T, object>> ignore, bool Async = true)
        {
            return Async ? await Db.Updateable<T>(model).IgnoreColumns(ignore).ExecuteCommandAsync()
                    : Db.Updateable<T>(model).IgnoreColumns(ignore).ExecuteCommand();
        }

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="parm">T</param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(List<T> parm, bool Async = true)
        {
            return Async ? await Db.Updateable<T>(parm).ExecuteCommandAsync() : Db.Updateable<T>(parm).ExecuteCommand();
        }

        /// <summary>
        /// 修改一条数据，可用作假删除
        /// </summary>
        /// <param name="columns">修改的列=Expression<Func<T,T>></param>
        /// <param name="where">Expression<Func<T,bool>></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(Expression<Func<T, T>> columns,
            Expression<Func<T, bool>> where, bool Async = true)
        {
            return Async ? await Db.Updateable<T>().SetColumns(columns).Where(where).ExecuteCommandAsync()
                    : Db.Updateable<T>().SetColumns(columns).Where(where).ExecuteCommand();
        }

       
        #endregion

        #region 删除操作
        /// <summary>
        /// 删除一条或多条数据
        /// </summary>
        /// <param name="parm">string</param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(string parm, bool Async = true)
        {
            var list = new List<long>();
            return Async ? await Db.Deleteable<T>().In(list.ToArray()).ExecuteCommandAsync() : Db.Deleteable<T>().In(list.ToArray()).ExecuteCommand();
        }

        /// <summary>
        /// 删除一条或多条数据
        /// </summary>
        /// <param name="where">Expression<Func<T, bool>></param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(Expression<Func<T, bool>> where, bool Async = true)
        {
            return Async ? await Db.Deleteable<T>().Where(where).ExecuteCommandAsync() : Db.Deleteable<T>().Where(where).ExecuteCommand();
        }
        #endregion

        #region 查询Count
        public async Task<int> CountAsync(Expression<Func<T, bool>> where, bool Async = true)
        {
            return Async ? await Db.Queryable<T>().CountAsync(where) : Db.Queryable<T>().Count(where);
        }
        #endregion

        #region 是否存在
        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> where, bool Async = true)
        {
            return Async ? await Db.Queryable<T>().AnyAsync(where) : Db.Queryable<T>().Any(where);
        }
        #endregion
    }
}
