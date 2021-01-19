using FytSoa.Domain.Models.Sys;
using FytSoa.Infra.Common.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Application.ViewModels
{
    /// <summary>
    /// 权限参数
    /// </summary>
    public class AuthorityMenuParam
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        [JsonConverter(typeof(ConverterExtension), ConverterExtensionShip.UInt64)]
        public long RoleId { get; set; }

        /// <summary>
        /// 授权菜单列表
        /// </summary>
        public List<AuthorityMenu> Menus { get; set; }
    }

    /// <summary>
    /// 授权菜单列表
    /// </summary>
    public class AuthorityMenu
    {
        /// <summary>
        /// 菜单Id
        /// </summary>
        [JsonConverter(typeof(ConverterExtension), ConverterExtensionShip.UInt64)]
        public long MenuId { get; set; }

        /// <summary>
        /// 菜单按钮权限
        /// </summary>
        public List<SysMenuBtnFun> BtnFun { get; set; }
    }
}
