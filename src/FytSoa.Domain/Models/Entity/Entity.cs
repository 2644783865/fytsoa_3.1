using Newtonsoft.Json;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Domain.Models
{
    public class Entity<TKey>
    {
        /// <summary>
        /// 唯一编号
        /// <summary>
        [SugarColumn(IsPrimaryKey = true)]
        [JsonConverter(typeof(string))]
        public virtual TKey Id { get; set; }
    }
}
