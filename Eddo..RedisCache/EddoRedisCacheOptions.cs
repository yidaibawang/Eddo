using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.RedisCache
{
    public   class EddoRedisCacheOptions
    {   

        /// <summary>
        /// 连接串key
        /// </summary>
        private const string ConnectionStringKey = "Eddo.Redis.Cache";
        /// <summary>
        /// 数据库编号
        /// </summary>
        private const string DatabaseIdSettingKey = "Eddo.Redis.Cache.DatabaseId";

        public string ConnectionString { get; set; }

        public int DatabaseId { get; set; }

        public EddoRedisCacheOptions()
        {

            ConnectionString = GetDefaultConnectionString();
            DatabaseId = GetDefaultDatabaseId();
        }
        /// <summary>
        /// 获取GetDefaultDatabaseId
        /// </summary>
        /// <returns></returns>
        private static int GetDefaultDatabaseId()
        {
#if NET45
            var appSetting = ConfigurationManager.AppSettings[DatabaseIdSettingKey];
            if (appSetting.IsNullOrEmpty())
            {
                return -1;
            }

            int databaseId;
            if (!int.TryParse(appSetting, out databaseId))
            {
                return -1;
            }

            return databaseId;
#else
            return -1;
#endif
        }
        /// <summary>
        /// 获取默认连接
        /// </summary>
        /// <returns></returns>
        private static string GetDefaultConnectionString()
        {
#if NET45
            var connStr = ConfigurationManager.ConnectionStrings[ConnectionStringKey];
            if (connStr == null || connStr.ConnectionString.IsNullOrWhiteSpace())
            {
                return "localhost";
            }

            return connStr.ConnectionString;
#else
            return "localhost";
#endif
        }
    }
}
