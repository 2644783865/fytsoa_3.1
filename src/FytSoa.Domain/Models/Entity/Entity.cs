using FytSoa.Infra.Common.Extensions;
using Newtonsoft.Json;
using SqlSugar;

namespace FytSoa.Domain.Models
{
    public class Entity<TKey>
    {
        /// <summary>
        /// 唯一编号
        /// <summary>
        [SugarColumn(IsPrimaryKey = true)]
        [JsonConverter(typeof(ConverterExtension), ConverterExtensionShip.UInt64)]
        public virtual TKey Id { get; set; }
    }
}
