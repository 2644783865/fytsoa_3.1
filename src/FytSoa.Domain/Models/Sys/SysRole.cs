using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;
using Newtonsoft.Json;

namespace FytSoa.Domain.Models.Sys
{
    /// <summary>
    /// 角色表
    /// </summary>
    [SugarTable("sys_role")]
    public class SysRole : EntityBase<string>
    {

        /// <summary>
        /// 角色名称
        /// <summary>
        public string Name { get; set; }

        /// <summary>
        /// 角色父节点
        /// <summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 角色层级
        /// <summary>
        public int Layer { get; set; } = 1;

        /// <summary>
        /// 角色编号
        /// <summary>
        public string Number { get; set; }

        /// <summary>
        /// 是否超级管理员
        /// <summary>
        public bool IsSystem { get; set; } = false;

        /// <summary>
        /// 状态
        /// <summary>
        public bool Status { get; set; } = true;

        /// <summary>
        /// 排序
        /// <summary>
        public int Sort { get; set; } = 1;

        /// <summary>
        /// 描述
        /// <summary>
        public string Summary { get; set; }

        /// <summary>
        /// 是否删除
        /// <summary>
        public bool IsDel { get; set; } = false;

    }
}