using System;
using System.Collections.Generic;
using System.Text;
using FytSoa.Infra.Common.Extensions;
using Newtonsoft.Json;
using SqlSugar;

namespace FytSoa.Domain.Models.Sys {
    /// <summary>
    /// 字典类型表
    /// </summary>
    [SugarTable ("sys_codetype")]
    public class SysCodeType : EntityBase<long> {

        /// <summary>
        /// 父节点
        /// <summary>
        [JsonConverter (typeof (ConverterExtension), ConverterExtensionShip.UInt64)]
        public long ParentId { get; set; }

        /// <summary>
        /// 层级
        /// <summary>
        public int Layer { get; set; } = 1;

        /// <summary>
        /// 分类名称
        /// <summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// <summary>
        public int Sort { get; set; } = 1;

        /// <summary>
        /// 是否系统内置集成
        /// <summary>
        public bool IsSystem { get; set; } = false;

        /// <summary>
        /// 是否删除
        /// <summary>
        public bool IsDel { get; set; } = false;

    }
}