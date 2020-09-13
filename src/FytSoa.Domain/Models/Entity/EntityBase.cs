using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Domain.Models
{
    public class EntityBase<TKey> : Entity<TKey>
    {
        /// <summary>
        /// 创建时间
        /// <summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 创建人
        /// <summary>
        public string CreateUser { get; set; }

        /// <summary>
        /// 修改时间
        /// <summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 修改人
        /// <summary>
        public string UpdateUser { get; set; }
    }
}
