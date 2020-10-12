using System;
using System.Collections.Generic;
using System.Text;
using Snowflake.Core;

namespace FytSoa.Infra.Common
{
    /// <summary>
    /// 唯一值  -  雪花算法
    /// </summary>
    public class Unique
    {
        private static Unique Instance = new Unique();
        private Unique() { }
        private static IdWorker worker;
        public static Unique GetInstance()
        {
            worker = new IdWorker(1, 1);
            return Instance;
        }

        /// <summary>
        /// 返回唯一ID值
        /// </summary>
        /// <returns></returns>
        public static string Id()
        {
            return worker.NextId().ToString();
        }
    }
}
