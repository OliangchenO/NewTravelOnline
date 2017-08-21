using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Belinda.Jasp
{
    /// <summary>
    /// 系统中所有状态定义EnumClass
    /// </summary>
    public class DictionaryEnum
    {
        public DictionaryEnum()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        /// <summary>
        /// xml节点类型
        /// </summary>
        public enum XmlNodeType
        {
            /// <summary>
            /// 添加
            /// </summary>
            [EnumDescription("Attribute")]
            Attribute,
            /// <summary>
            /// 修改
            /// </summary>
            [EnumDescription("Node")]
            Node
        }
        /// <summary>
        /// 性别
        /// </summary>
        public enum SEX
        {
            /// <summary>
            /// 女
            /// </summary>
            [EnumDescription("女")]
            False,
            /// <summary>
            /// 男
            /// </summary>
            [EnumDescription("男")]
            True
        }
        public enum Direction
        {/// <summary>
            /// DESC
            /// </summary>
            [EnumDescription("DESC")]
            DESC,
            /// <summary>
            /// ASC
            /// </summary>
            [EnumDescription("ASC")]
            ASC
        }

    }
    /// <summary>
    /// 日志优先级
    /// </summary>
    public enum LogPriority
    {
        /// <summary>
        /// 最低
        /// </summary>
        Lowest = 0,
        /// <summary>
        /// 低
        /// </summary>
        Low = 1,
        /// <summary>
        /// 普通
        /// </summary>
        Normal = 2,
        /// <summary>
        /// 高
        /// </summary>
        High = 3,
        /// <summary>
        /// 最高
        /// </summary>
        Highest = 4
    }

    /// <summary>
    /// 错误日志类型枚举
    /// </summary>
    public enum LogCategory
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,
        /// <summary>
        /// 线程错误
        /// </summary>
        ThreadingErrer = 1,
        /// <summary>
        /// 异常
        /// </summary>
        Exception = 2
    }
}