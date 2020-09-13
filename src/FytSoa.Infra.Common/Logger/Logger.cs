using System;
using NLog;
using NLog.Web;

namespace FytSoa.Infra.Common.Logger
{
    public class Logger
    {
        NLog.Logger _logger;
        private Logger(NLog.Logger logger)
        {
            _logger = logger;
        }
        public Logger(string name) : this(LogManager.GetLogger(name))
        {

        }
        public static Logger Default { get; private set; }

        static Logger()
        {
            Default = new Logger(LogManager.GetCurrentClassLogger());
        }

        private static string _path = "";

        /// <summary>
        /// 自定义输出目录，初始化
        /// </summary>
        public void Setting(string path)
        {
            if (_path != path)
            {
                _path = path;
                LogManager.Configuration.Variables["cuspath"] = path + "/";
            }
        }

        #region Info
        /// <summary>
        /// 最常见的记录信息，一般用于普通输出
        /// </summary>
        /// <param name="msg">msg</param>
        public void Info(string msg)
        {
            _logger.WithProperty("filename", _path).Info(msg);
        }

        /// <summary>
        /// 最常见的记录信息，一般用于普通输出
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <param name="msg">msg</param>
        public void Info(Exception ex, string msg)
        {
            _logger.WithProperty("filename", _path).Info(ex,msg);
        }

        /// <summary>
        /// 最常见的记录信息，一般用于普通输出
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        public void Info<T>(T value)
        {
            _logger.WithProperty("filename", _path).Info(value);
        }
        #endregion

        #region Debug
        /// <summary>
        /// 最常见的记录信息，一般用于普通输出
        /// </summary>
        /// <param name="msg">msg</param>
        public void Debug(string msg)
        {
            _logger.Debug(msg);
        }

        /// <summary>
        /// 最常见的记录信息，一般用于普通输出
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <param name="msg">msg</param>
        public void Debug(Exception ex, string msg)
        {
            _logger.Debug(ex, msg);
        }

        /// <summary>
        /// 最常见的记录信息，一般用于普通输出
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        public void Debug<T>(T value)
        {
            _logger.Debug(value);
        }
        #endregion

        #region Error
        /// <summary>
        /// 最常见的记录信息，一般用于普通输出
        /// </summary>
        /// <param name="msg">msg</param>
        public void Error(string msg)
        {
            _logger.Error(msg);
        }

        /// <summary>
        /// 最常见的记录信息，一般用于普通输出
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <param name="msg">msg</param>
        public void Error(Exception ex, string msg)
        {
            _logger.Error(ex, msg);
        }

        /// <summary>
        /// 最常见的记录信息，一般用于普通输出
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        public void Error<T>(T value)
        {
            _logger.Error(value);
        }
        #endregion
    }
}
