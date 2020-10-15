using FytSoa.Infra.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FytSoa.Domain.Repository.Interfaces
{
    /// <summary>
    /// 定义基本服务
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepository<T>  where T : class
    {
        #region 添加操作
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="parm">cms_advlist</param>
        /// <returns></returns>
        Task<int> AddAsync(T parm);

        /// <summary>
        /// 批量添加数据
        /// </summary>
        /// <param name="parm">List<T></param>
        /// <returns></returns>
        Task<int> AddListAsync(List<T> parm);

        #endregion

        #region 查询操作
        /// <summary>
        /// 根据条件查询列表
        /// </summary>
        /// <param name="where">拉姆达条件</param>
        /// <param name="order">拉姆达排序</param>
        /// <param name="orderEnum">枚举，1=desc 2=asc</param>
        /// <param name="Async">是否同步</param>
        /// <returns></returns>
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> where,
            Expression<Func<T, object>> order, int orderEnum);

        /// <summary>
        /// 获得列表
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetListAsync();

        /// <summary>
        /// 获得一条数据
        /// </summary>
        /// <param name="parm">string</param>
        /// <returns></returns>
        Task<T> GetModelAsync(string parm);

        /// <summary>
        /// 获得一条数据
        /// </summary>
        /// <param name="where">Expression<Func<T, bool>></param>
        /// <returns></returns>
        Task<T> GetModelAsync(Expression<Func<T, bool>> where);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <param name="orderEnum">枚举，1=desc 2=asc</param>
        /// <param name="page">当前页</param>
        /// <param name="limit">每页条数</param>
        /// <returns></returns>
        Task<PageResult<T>> GetPageResult(Expression<Func<T, bool>> where,Expression<Func<T, object>> order, int orderEnum,int page,int limit);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="page">当前页</param>
        /// <param name="limit">每页条数</param>
        /// <returns></returns>
        Task<PageResult<T>> GetPageResult(Expression<Func<T, bool>> where, string orderby, int page, int limit);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="page">当前页</param>
        /// <param name="limit">每页条数</param>
        /// <returns></returns>
        Task<PageResult<T>> GetPageResult(string where, string orderby, int page, int limit);
        #endregion

        #region 修改操作
        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <param name="parm">T</param>
        /// <returns></returns>
        Task<int> UpdateAsync(T parm);

        /// <summary>
        /// 修改一条数据，增加忽略项
        /// </summary>
        /// <param name="model">T</param>
        /// <param name="ignore">忽略列，如new {it.id,it.name}</param>
        /// <returns></returns>
        Task<int> UpdateAsync(T model,
            Expression<Func<T, object>> ignore);

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <param name="parm">T</param>
        /// <returns></returns>
        Task<int> UpdateAsync(List<T> parm);

        /// <summary>
        /// 修改一条数据，可用作假删除
        /// </summary>
        /// <param name="columns">修改的列=Expression<Func<T,T>></param>
        /// <param name="where">Expression<Func<T,bool>></param>
        /// <returns></returns>
        Task<int> UpdateAsync(Expression<Func<T, T>> columns,
            Expression<Func<T, bool>> where);
        #endregion

        #region 删除操作
        /// <summary>
        /// 删除一条或多条数据
        /// </summary>
        /// <param name="parm">string</param>
        /// <returns></returns>
        Task<int> DeleteAsync(string parm);

        /// <summary>
        /// 删除一条或多条数据
        /// </summary>
        /// <param name="where">Expression<Func<T, bool>></param>
        /// <returns></returns>
        Task<int> DeleteAsync(Expression<Func<T, bool>> where);

        #endregion

        #region 查询Count
        Task<int> CountAsync(Expression<Func<T, bool>> where);
        #endregion

        #region 是否存在
        Task<bool> IsExistAsync(Expression<Func<T, bool>> where);
        #endregion
    }
}
