using Eddo.Reflection;
using System;
using System.Reflection;

namespace Eddo.Domain.Entities
{
    public static class EntityHelper
    {   
        /// <summary>
        /// 是否为字段
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsEntity(Type type)
        {
            return ReflectionHelper.IsAssignableToGenericType(type, typeof(IEntity<>));
        }
        /// <summary>
        /// 主键类型
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public static Type GetPrimaryKeyType<TEntity>()
        {
            return GetPrimaryKeyType(typeof(TEntity));
        }

        /// <summary>
        /// Gets primary key type of given entity type
        /// </summary>
        public static Type GetPrimaryKeyType(Type entityType)
        {
            foreach (var interfaceType in entityType.GetTypeInfo().GetInterfaces())
            {
                if (interfaceType.GetTypeInfo().IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(IEntity<>))
                {
                    return interfaceType.GenericTypeArguments[0];
                }
            }

            throw new Exception("无法找到给定实体类型的主键类型:  " + entityType + "。确保此实体类型实现<TPrimaryKey>接口");
        }
    }
}
