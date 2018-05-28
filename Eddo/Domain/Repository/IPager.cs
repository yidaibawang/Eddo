﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Domain.Repository
{
    public interface IPager: IPagerBase
    {
        /// <summary>
        /// 获取总页数
        /// </summary>
        int GetPageCount();
        /// <summary>
        /// 获取跳过的行数
        /// </summary>
        int GetSkipCount();
        /// <summary>
        /// 排序条件
        /// </summary>
        string Order { get; set; }
        /// <summary>
        /// 获取起始行数
        /// </summary>
        int GetStartNumber();
        /// <summary>
        /// 获取结束行数
        /// </summary>
        int GetEndNumber();
    }
}
