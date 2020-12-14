using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;
using Newtonsoft.Json;

namespace FytSoa.Domain.Models.Sys
{
    /// <summary>
    /// 组织机构表
    /// </summary>
    [SugarTable("sys_organize")]
    public class SysOrganize : EntityBase<long>
    {

        /// <summary>
        /// 父节点
        /// <summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 部门名称
        /// <summary>
        public string Name { get; set; }

        /// <summary>
        /// 父节点集合
        /// <summary>
        public string ParentIdList { get; set; }

        /// <summary>
        /// 部门层级
        /// <summary>
        public int Layer { get; set; } = 0;

        /// <summary>
        /// 排序
        /// <summary>
        public int Sort { get; set; } = 1;

        /// <summary>
        /// 状态
        /// <summary>
        public bool Status { get; set; } = true;

        /// <summary>
        /// 删除状态
        /// <summary>
        public bool IsDel { get; set; } = false;

        /// <summary>
        /// 部门负责人
        /// <summary>
        public string LeaderUser { get; set; }

        /// <summary>
        /// 联系电话
        /// <summary>
        public string LeaderMobile { get; set; }

        /// <summary>
        /// 联系邮箱
        /// <summary>
        public string LeaderEmail { get; set; }

    }
}