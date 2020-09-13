using System;
using System.Collections.Generic;
using System.Text;
using FytSoa.Infra.Common;
using Newtonsoft.Json;
using System.Linq;
using SqlSugar;
using FytSoa.Infra.Common.Logger;

namespace FytSoa.Infra.Data.Context
{
    public class SugarDbContext
    {
        public SqlSugarClient Db;
        public SugarDbContext()
        {
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = AppSettingConfig.MySqlConnectionString,
                DbType = DbType.MySql,
                IsAutoCloseConnection = true
            });
            Db.Ado.CommandTimeOut = 30000;//设置超时时间
            Db.Aop.OnLogExecuted = (sql, pars) => //SQL执行完事件
            {
#if DEBUG
                //获取执行后的总毫秒数
                double sqlExecutionTotalMilliseconds = Db.Ado.SqlExecutionTime.TotalMilliseconds;
#endif
            };
            Db.Aop.OnLogExecuting = (sql, pars) => //SQL执行前事件
            {
#if DEBUG
                string tempSQL = LookSQL(sql, pars);
#endif
            };
            Db.Aop.OnError = (exp) =>//执行SQL 错误事件
            {
                //exp.sql exp.parameters 可以拿到参数和错误Sql  
                StringBuilder sb_SugarParameterStr = new StringBuilder("###SugarParameter参数为:");
                var parametres = exp.Parametres as SugarParameter[];
                if (parametres != null)
                {
                    sb_SugarParameterStr.Append(JsonConvert.SerializeObject(parametres));
                }

                StringBuilder sb_error = new StringBuilder();
                sb_error.AppendLine("SqlSugarClient执行sql异常信息:" + exp.Message);
                sb_error.AppendLine("###赋值后sql:" + LookSQL(exp.Sql, parametres));
                sb_error.AppendLine("###带参数的sql:" + exp.Sql);
                sb_error.AppendLine("###参数信息:" + sb_SugarParameterStr.ToString());
                sb_error.AppendLine("###StackTrace信息:" + exp.StackTrace);

                Logger.Default.Setting("SqlError");
                Logger.Default.Error(sb_error.ToString());
                Logger.Default.Setting("");
            };
            Db.Aop.OnExecutingChangeSql = (sql, pars) => //SQL执行前 可以修改SQL
            {
                return new KeyValuePair<string, SugarParameter[]>(sql, pars);
            };
            Db.Aop.OnDiffLogEvent = it =>// 数据库操作前和操作后的数据变化。
            {
                var editBeforeData = it.BeforeData;
                var editAfterData = it.AfterData;
                var sql = it.Sql;
                var parameter = it.Parameters;
                var data = it.BusinessData;
                var time = it.Time;
                var diffType = it.DiffType;//枚举值 insert 、update 和 delete 用来作业务区分

                //保存修改操作的数据变化
                if (diffType == DiffType.update)
                {

                }
            };
        }

        /// <summary>
        /// 查看赋值后的sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pars">参数</param>
        /// <returns></returns>
        private string LookSQL(string sql, SugarParameter[] pars)
        {
            if (pars == null || pars.Length == 0) return sql;

            StringBuilder sb_sql = new StringBuilder(sql);
            var tempOrderPars = pars.Where(p => p.Value != null).OrderByDescending(p => p.ParameterName.Length).ToList();//防止 @par1错误替换@par12
            for (var index = 0; index < tempOrderPars.Count; index++)
            {
                sb_sql.Replace(tempOrderPars[index].ParameterName, "'" + tempOrderPars[index].Value.ToString() + "'");
            }

            return sb_sql.ToString();
        }
    }
}
