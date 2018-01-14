﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebApiClient
{
    /// <summary>
    /// 表示格式化选项
    /// </summary>
    public class FormatOptions
    {
        /// <summary>
        /// 日期时间格式
        /// </summary>
        private string dateTimeFormat;

        /// <summary>
        /// 获取或设置是否使用骆驼命名
        /// 默认为false
        /// </summary>
        public bool UseCamelCase { get; set; }

        /// <summary>
        /// 获取或设置时期时间格式
        /// 默认为本地日期时间格式
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public string DateTimeFormat
        {
            get
            {
                return this.dateTimeFormat;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }
                this.dateTimeFormat = value;
            }
        }

        /// <summary>
        /// 格式化选项
        /// </summary>
        public FormatOptions()
        {
            this.UseCamelCase = false;
            this.DateTimeFormat = DateTimeFormats.GetLocalDateTimeFormat();
        }

        /// <summary>
        /// 如果新的日期时间格式有变化
        /// 则克隆并使用新的datetimeFormat
        /// </summary>
        /// <param name="datetimeFormat">日期时间格式</param>
        /// <returns></returns>
        public FormatOptions CloneChange(string datetimeFormat)
        {
            if (string.IsNullOrEmpty(datetimeFormat) || string.Equals(this.DateTimeFormat, datetimeFormat))
            {
                return this;
            }

            return new FormatOptions
            {
                DateTimeFormat = datetimeFormat,
                UseCamelCase = this.UseCamelCase
            };
        }

        /// <summary>
        /// 骆驼命名
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public static string CamelCase(string name)
        {
            if (string.IsNullOrEmpty(name) || char.IsUpper(name[0]) == false)
            {
                return name;
            }

            var charArray = name.ToCharArray();
            for (int i = 0; i < charArray.Length; i++)
            {
                if (i == 1 && char.IsUpper(charArray[i]) == false)
                {
                    break;
                }

                var hasNext = (i + 1 < charArray.Length);
                if (i > 0 && hasNext && !char.IsUpper(charArray[i + 1]))
                {                    
                    if (char.IsSeparator(charArray[i + 1]))
                    {
                        charArray[i] = Char.ToLowerInvariant(charArray[i]);
                    }
                    break;
                }
                charArray[i] = Char.ToLowerInvariant(charArray[i]);
            }
            return new string(charArray);
        }
    }
}