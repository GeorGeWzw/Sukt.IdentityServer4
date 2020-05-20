using System;
using System.Collections.Generic;
using System.Text;

namespace MOSI.IdentityServer4.Models.ConsulModel
{
    /// <summary>
    /// Consul服務治理實體
    /// </summary>
    public class ConsulServiceEntity
    {
        /// <summary>
        /// 服務IP
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 服務端口
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 服務名稱
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// Consul治理中心IP
        /// </summary>
        public string ConsulIP { get; set; }
        /// <summary>
        /// 治理中心端口
        /// </summary>
        public int ConsulPort { get; set; }
    }
}
