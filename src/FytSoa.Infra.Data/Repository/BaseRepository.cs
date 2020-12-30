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
    public class BaseRepository<T> : SugarDbContext, IBaseRepository<T> where T : class, new()
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
        public async Task<int> AddAsync(T parm)
        {
            return await Db.Insertable<T>(parm).ExecuteCommandAsync();
        }

        /// <summary>
        /// 批量添加数据
        /// </summary>
        /// <param name="parm">List<T></param>
        /// <returns></returns>
        public async Task<int> AddListAsync(List<T> parm)
        {
            return await Db.Insertable<T>(parm).ExecuteCommandAsync();
        }
        #endregion

        #region 查询操作
        /// <summary>
        /// 获得一条数据
        /// </summary>
        /// <param name="where">Expression<Func<T, bool>></param>
        /// <returns></returns>
        public async Task<T> GetModelAsync(Expression<Func<T, bool>> where)
        {
            return await Db.Queryable<T>().Where(where).FirstAsync() ?? new T() { };
        }

        /// <summary>
        /// 获得一条数据
        /// </summary>
        /// <param name="parm">string</param>
        /// <returns></returns>
        public async Task<T> GetModelAsync(string parm)
        {
            return await Db.Queryable<T>().Where(parm).FirstAsync() ?? new T() { };
        }

        /// <summary>
        /// 根据条件，获得最新的一条数据
        /// </summary>
        /// <param name="where">拉姆达条件</param>
        /// <param name="order">拉姆达排序</param>
        /// <param name="orderEnum">枚举，1=desc 2=asc</param>
        /// <returns></returns>
        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> where, Expression<Func<T, object>> order, int orderEnum)
        {
            return await Db.Queryable<T>()
                .Where(where)
                .OrderByIF(orderEnum == 1, order, OrderByType.Desc)
                .OrderByIF(orderEnum == 2, order, OrderByType.Asc)
                .FirstAsync() ?? new T() { };
        }

        /// <summary>
        /// 根据条件查询列表
        /// </summary>
        /// <param name="where">拉姆达条件</param>
        /// <param name="order">拉姆达排序</param>
        /// <param name="orderEnum">枚举，1=desc 2=asc</param>
        /// <returns></returns>
        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> where,
            Expression<Func<T, object>> order, int orderEnum)
        {
            var query = Db.Queryable<T>()
                        .Where(where)
                        .OrderByIF(orderEnum == 1, order, OrderByType.Desc)
                        .OrderByIF(orderEnum == 2, order, OrderByType.Asc);
            return await query.ToListAsync();
        }

        /// <summary>
        /// 根据条件查询列表
        /// </summary>
        /// <param name="where">拉姆达条件</param>
        /// <param name="order">order by id desc</param>
        /// <returns></returns>
        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> where,
            string order)
        {
            var query = Db.Queryable<T>()
                        .Where(where)
                        .OrderBy(order);
            return await query.ToListAsync();
        }

        /// <summary>
        /// 查询所有，只处理排序
        /// </summary>
        /// <param name="order">拉姆达排序</param>
        /// <param name="orderEnum">枚举，1=desc 2=asc</param>
        /// <returns></returns>
        public async Task<List<T>> GetListAsync(Expression<Func<T, object>> order, int orderEnum)
        {
            var query = Db.Queryable<T>()
                        .OrderByIF(orderEnum == 1, order, OrderByType.Desc)
                        .OrderByIF(orderEnum == 2, order, OrderByType.Asc);
            return await query.ToListAsync();
        }

        /// <summary>
        /// 根据条件查询列表
        /// </summary>
        /// <param name="order">order by id desc</param>
        /// <returns></returns>
        public async Task<List<T>> GetListAsync(string order)
        {
            var query = Db.Queryable<T>()
                        .OrderBy(order);
            return await query.ToListAsync();
        }

        /// <summary>
        /// 获得列表，不需要任何条件
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> GetListAsync()
        {
            return await Db.Queryable<T>().ToListAsync();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <param name="orderEnum">枚举，1=desc 2=asc</param>
        /// <param name="page">当前页</param>
        /// <param name="limit">每页条数</param>
        /// <returns></returns>
        public async Task<PageResult<T>> GetPageResult(Expression<Func<T, bool>> where, Expression<Func<T, object>> order, int orderEnum, int page, int limit)
        {
            var result = new PageResult<T>();
            RefAsync<int> totalItems = 0;
            result.Items = orderEnum == 1
                ? await Db.Queryable<T>().Where(where).OrderBy(order, OrderByType.Desc).ToPageListAsync(page, limit, totalItems)
                : await Db.Queryable<T>().Where(where).OrderBy(order, OrderByType.Asc).ToPageListAsync(page, limit, totalItems);
            result.CurrentPage = page;
            result.ItemsPerPage = limit;
            result.TotalItems = totalItems;
            result.TotalPages = totalItems != 0 ? (totalItems % page) == 0 ? (totalItems / limit) : (totalItems / limit) + 1 : 0;
            return result;
        }

        /// <summary>
        /// 分页查询 + 自定义返回结果
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <param name="orderEnum">枚举，1=desc 2=asc</param>
        /// <param name="page">当前页</param>
        /// <param name="limit">每页条数</param>
        /// <returns></returns>
        public async Task<PageResult<T>> GetPageResult(Expression<Func<T, bool>> where, Expression<Func<T, object>> order, Expression<Func<T, T>> selectColumn, int orderEnum, int page, int limit)
        {
            var result = new PageResult<T>();
            RefAsync<int> totalItems = 0;
            result.Items = orderEnum == 1
                ? await Db.Queryable<T>().Where(where).OrderBy(order, OrderByType.Desc).Select(selectColumn).ToPageListAsync(page, limit, totalItems)
                : await Db.Queryable<T>().Where(where).OrderBy(order, OrderByType.Asc).Select(selectColumn).ToPageListAsync(page, limit, totalItems);
            result.CurrentPage = page;
            result.ItemsPerPage = limit;
            result.TotalItems = totalItems;
            result.TotalPages = totalItems != 0 ? (totalItems % page) == 0 ? (totalItems / limit) : (totalItems / limit) + 1 : 0;
            return result;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="page">当前页</param>
        /// <param name="limit">每页条数</param>
        /// <returns></returns>
        public async Task<PageResult<T>> GetPageResult(Expression<Func<T, bool>> where, string orderby, int page, int limit)
        {
            var result = new PageResult<T>();
            RefAsync<int> totalItems = 0;
            result.Items = await Db.Queryable<T>().Where(where).OrderBy(orderby).ToPageListAsync(page, limit, totalItems);
            result.CurrentPage = page;
            result.ItemsPerPage = limit;
            result.TotalItems = totalItems;
            result.TotalPages = totalItems != 0 ? (totalItems % page) == 0 ? (totalItems / limit) : (totalItems / limit) + 1 : 0;
            return result;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="page">当前页</param>
        /// <param name="limit">每页条数</param>
        /// <returns></returns>
        public async Task<PageResult<T>> GetPageResult(string where, string orderby, int page, int limit)
        {
            var result = new PageResult<T>();
            RefAsync<int> totalItems = 0;
            result.Items = await Db.Queryable<T>().Where(where).OrderBy(orderby).ToPageListAsync(page, limit, totalItems);
            result.CurrentPage = page;
            result.ItemsPerPage = limit;
            result.TotalItems = totalItems;
            result.TotalPages = totalItems != 0 ? (totalItems % page) == 0 ? (totalItems / limit) : (totalItems / limit) + 1 : 0;
            return result;
        }
        #endregion

        #region 修改操作
        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <param name="parm">T</param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(T parm)
        {
            return await Db.Updateable<T>(parm).ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改一条数据，增加忽略项
        /// </summary>
        /// <param name="model">T</param>
        /// <param name="ignore">忽略列，如new {it.id,it.name}</param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(T model,
            Expression<Func<T, object>> ignore)
        {
            return await Db.Updateable<T>(model).IgnoreColumns(ignore).ExecuteCommandAsync();
        }

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="parm">T</param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(List<T> parm)
        {
            return await Db.Updateable<T>(parm).ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改一条数据，可用作假删除
        /// </summary>
        /// <param name="columns">修改的列=Expression<Func<T,T>></param>
        /// <param name="where">Expression<Func<T,bool>></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(Expression<Func<T, T>> columns,
            Expression<Func<T, bool>> where)
        {
            return await Db.Updateable<T>().SetColumns(columns).Where(where).ExecuteCommandAsync();
        }


        #endregion

        #region 删除操作
        /// <summary>
        /// 删除一条或多条数据
        /// </summary>
        /// <param name="parm">string</param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(string parm)
        {
            var list = new List<long>();
            return await Db.Deleteable<T>().In(list.ToArray()).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除一条或多条数据
        /// </summary>
        /// <param name="where">Expression<Func<T, bool>></param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(Expression<Func<T, bool>> where)
        {
            return await Db.Deleteable<T>().Where(where).ExecuteCommandAsync();
        }
        #endregion

        #region 查询Count
        public async Task<int> CountAsync(Expression<Func<T, bool>> where)
        {
            return await Db.Queryable<T>().CountAsync(where);
        }
        #endregion

        #region 是否存在
        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> where)
        {
            return await Db.Queryable<T>().AnyAsync(where);
        }
        #endregion
    }
}
