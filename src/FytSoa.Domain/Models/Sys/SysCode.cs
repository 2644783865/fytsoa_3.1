using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;
using Newtonsoft.Json;

namespace FytSoa.Domain.Models.Sys
{
    /// <summary>
    /// 字典值表
    /// </summary>
    [SugarTable("sys_code")]
    public class SysCode : EntityBase<string>
    {

        /// <summary>
        /// 分类编号
        /// <summary>
        public string TypeId { get; set; }

        /// <summary>
        /// 字典值名称
        /// <summary>
        public string Name { get; set; }

        /// <summary>
        /// 字典值阈值
        /// <summary>
        public string CodeValues { get; set; }

        /// <summary>
        /// 排序
        /// <summary>
        public int Sort { get; set; } = 1;

        /// <summary>
        /// 状态
        /// <summary>
        public bool Status { get; set; } = true;

        /// <summary>
        /// 是否删除
        /// <summary>
        public bool IsDel { get; set; } = false;

        /// <summary>
        /// 备注
        /// <summary>
        public string Summary { get; set; }

    }
}