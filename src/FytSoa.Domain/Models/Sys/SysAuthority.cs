using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;
using Newtonsoft.Json;

namespace FytSoa.Domain.Models.Sys
{
    /// <summary>
    /// 授权信息表
    /// </summary>
    [SugarTable("sys_authority")]
    public class SysAuthority
    {
        /// <summary>
        /// 角色编号
        /// <summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 管理员编号
        /// <summary>
        public string AdminId { get; set; }

        /// <summary>
        /// 菜单编号
        /// <summary>
        public string MenuId { get; set; }

        /// <summary>
        /// 按钮功能组
        /// <summary>
        public string BtnFun { get; set; }

        /// <summary>
        /// 授权类型1=角色-菜单 2=用户-角色 3=角色-菜单-按钮功能
        /// <summary>
        public int Types { get; set; } = 1;


    }
}