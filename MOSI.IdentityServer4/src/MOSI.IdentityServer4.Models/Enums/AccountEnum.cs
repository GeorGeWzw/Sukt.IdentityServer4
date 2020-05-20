using System;
using System.Collections.Generic;
using System.Text;

namespace MOSI.IdentityServer4.Models
{
    public enum AccountEnum
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 锁定
        /// </summary>
        Locking = 2,
        /// <summary>
        /// 禁用
        /// </summary>
        Prohibit = 3
    }
}
