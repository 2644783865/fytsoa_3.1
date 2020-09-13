using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FytSoa.Domain.Models.Sys
{
    /// <summary>
    /// 管理员表
    /// </summary>
    [SugarTable("sys_admin")]
    public class SysAdmin : EntityBase<long>
    {
        /// <summary>
        /// 所属角色
        /// <summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 所属岗位
        /// <summary>
        public string PostId { get; set; }

        /// <summary>
        /// 所属部门
        /// <summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 所属上级部门组
        /// <summary>
        public string OrganizeGroupId { get; set; }

        /// <summary>
        /// 登录账号
        /// <summary>
        public string LoginAccount { get; set; }

        /// <summary>
        /// 登录密码
        /// <summary>
        private string _loginPassWord;
        public string LoginPassWord {
            get { return !string.IsNullOrEmpty(_loginPassWord)?Common.DES3Encrypt.DecryptString(_loginPassWord):_loginPassWord; }

            set { _loginPassWord = value; }
        }

        /// <summary>
        /// 头像
        /// <summary>
        public string HeadPic { get; set; }

        /// <summary>
        /// 姓名
        /// <summary>
        public string FullName { get; set; }

        /// <summary>
        /// 手机号码
        /// <summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 邮箱
        /// <summary>
        public string Email { get; set; }

        /// <summary>
        /// 性别
        /// <summary>
        public string Sex { get; set; }

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

        /// <summary>
        /// 登录时间
        /// <summary>
        public DateTime? LoginTime { get; set; }

        /// <summary>
        /// 上次登录时间
        /// <summary>
        public DateTime? UpLoginTime { get; set; }

        /// <summary>
        /// 登录次数
        /// <summary>
        public int LoginCount { get; set; } = 0;

        /// <summary>
        /// 部门
        /// <summary>
        [SugarColumn(IsIgnore = true)]
        public SysOrganize organize { get; set; }
    }
}