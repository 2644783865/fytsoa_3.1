using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;
using Newtonsoft.Json;

namespace FytSoa.Domain.Models.Sys
{
    /// <summary>
    /// 岗位表
    /// </summary>
    [SugarTable("sys_post")]
    public class SysPost : EntityBase<string>
    {

        /// <summary>
        /// 岗位名称
        /// <summary>
        public string Name { get; set; }

        /// <summary>
        /// 岗位编码
        /// <summary>
        public string Number { get; set; }

        /// <summary>
        /// 排序
        /// <summary>
        public int Sort { get; set; } = 1;

        /// <summary>
        /// 岗位状态
        /// <summary>
        public bool Status { get; set; } = true;

        /// <summary>
        /// 删除状态
        /// <summary>
        public bool IsDel { get; set; } = false;

        /// <summary>
        /// 备注信息
        /// <summary>
        public string Summary { get; set; }

    }
}