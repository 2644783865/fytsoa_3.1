using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FytSoa.Infra.Common
{
    /// <summary>
    /// AutoMapper的帮助类
    /// </summary>
    public static class AutoMapper
    {

        /// <summary>
        /// 单条实体类型映射,默认字段名字一一对应
        /// </summary>
        /// <typeparam name="TSource">Dto类型</typeparam>
        /// <typeparam name="TDestination">要被转化的数据</typeparam>
        /// <param name="source">转化之后的实体</param>
        /// <param name="ignore">要忽略的属性</param>
        /// <returns></returns>
        public static TDestination MapTo<TSource, TDestination>(this TSource source, Expression<Func<TDestination, object>> ignore = null)
            where TDestination : class, new()
            where TSource : class
        {
            if (source == null) return new TDestination();
            MapperConfiguration config = null;
            if (ignore == null)
            {
                config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
            }
            else
            {
                config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>().ForMember(ignore, c => c.Ignore()));
            }
            var mapper = config.CreateMapper();
            return mapper.Map<TDestination>(source);
        }

        /// <summary>
        /// 实体列表类型映射,默认字段名字一一对应
        /// </summary>
        /// <typeparam name="TDestination">Dto类型</typeparam>
        /// <typeparam name="TSource">要被转化的数据</typeparam>
        /// <param name="source">可以使用这个扩展方法的类型，任何引用类型</param>
        /// <param name="ignore">要忽略的属性</param>
        /// <returns>转化之后的实体列表</returns>
        public static List<TDestination> MapToList<TSource, TDestination>(this IEnumerable<TSource> source, Expression<Func<TDestination, object>> ignore = null)
            where TDestination : class
            where TSource : class
        {
            if (source == null) return new List<TDestination>();
            MapperConfiguration config = null;
            if (ignore == null)
            {
                config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
            }
            else
            {
                config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>().ForMember(ignore, c => c.Ignore()));
            }
            var mapper = config.CreateMapper();

            return mapper.Map<List<TDestination>>(source);
        }

        /// <summary>
        /// 扩展忽略属性
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="map"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> map, Expression<Func<TDestination, object>> selector)
        {
            map.ForMember(selector, config => config.Ignore());
            return map;
        }


    }
}
