using System;
using System.Collections.Generic;
using System.Text;
using FytSoa.Infra.Common.Extensions;
using Newtonsoft.Json;
using SqlSugar;

namespace FytSoa.Domain.Models.Sys {
    /// <summary>
    /// 授权信息表
    /// </summary>
    [SugarTable ("sys_authority")]
    public class SysAuthority {
        /// <summary>
        /// 角色编号
        /// <summary>
        [JsonConverter (typeof (ConverterExtension), ConverterExtensionShip.UInt64)]
        public long RoleId { get; set; }

        /// <summary>
        /// 管理员编号
        /// <summary>
        [JsonConverter (typeof (ConverterExtension), ConverterExtensionShip.UInt64)]
        public long AdminId { get; set; }

        /// <summary>
        /// 菜单编号
        /// <summary>
        [JsonConverter (typeof (ConverterExtension), ConverterExtensionShip.UInt64)]
        public long MenuId { get; set; }

        /// <summary>
        /// 按钮功能组
        /// <summary>
        [SugarColumn(IsJson = true)]
        public List<SysMenuBtnFun> BtnFun { get; set; }

        /// <summary>
        /// 授权类型1=角色-菜单 2=用户-角色 3=角色-菜单-按钮功能
        /// <summary>
        public int Types { get; set; } = 1;

    }
}