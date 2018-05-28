﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Dependency
{
    public interface IIocResolve
    {
        /// <summary>
        /// 获取ioc容器
        /// Returning object must be Released (see <see cref="Release"/>) after usage.
        /// </summary> 
        /// <typeparam name="T">Type of the object to get</typeparam>
        /// <returns>The object instance</returns>
        T Resolve<T>();

        /// <summary>
        ///根据类型恢复
        /// Returning object must be Released (see <see cref="Release"/>) after usage.
        /// </summary> 
        /// <typeparam name="T">Type of the object to cast</typeparam>
        /// <param name="type">Type of the object to resolve</param>
        /// <returns>The object instance</returns>
        T Resolve<T>(Type type);

        /// <summary>
        /// Gets an object from IOC container.
        /// Returning object must be Released (see <see cref="Release"/>) after usage.
        /// </summary> 
        /// <typeparam name="T">Type of the object to get</typeparam>
        /// <param name="argumentsAsAnonymousType">Constructor arguments</param>
        /// <returns>The object instance</returns>
        T Resolve<T>(object argumentsAsAnonymousType);

        /// <summary>
        /// Gets an object from IOC container.
        /// Returning object must be Released (see <see cref="Release"/>) after usage.
        /// </summary> 
        /// <param name="type">Type of the object to get</param>
        /// <returns>The object instance</returns>
        object Resolve(Type type);

        /// <summary>
        /// Gets an object from IOC container.
        /// Returning object must be Released (see <see cref="Release"/>) after usage.
        /// </summary> 
        /// <param name="type">Type of the object to get</param>
        /// <param name="argumentsAsAnonymousType">Constructor arguments</param>
        /// <returns>The object instance</returns>
        object Resolve(Type type, object argumentsAsAnonymousType);

        /// <summary>
        /// 恢复所有
        /// Returning objects must be Released (see <see cref="Release"/>) after usage.
        /// </summary> 
        /// <typeparam name="T">Type of the objects to resolve</typeparam>
        /// <returns>Object instances</returns>
        T[] ResolveAll<T>();

        /// <summary>
        /// Gets all implementations for given type.
        /// Returning objects must be Released (see <see cref="Release"/>) after usage.
        /// </summary> 
        /// <typeparam name="T">Type of the objects to resolve</typeparam>
        /// <param name="argumentsAsAnonymousType">Constructor arguments</param>
        /// <returns>Object instances</returns>
        T[] ResolveAll<T>(object argumentsAsAnonymousType);

        /// <summary>
        /// Gets all implementations for given type.
        /// Returning objects must be Released (see <see cref="Release"/>) after usage.
        /// </summary> 
        /// <param name="type">Type of the objects to resolve</param>
        /// <returns>Object instances</returns>
        object[] ResolveAll(Type type);

        /// <summary>
        /// Gets all implementations for given type.
        /// Returning objects must be Released (see <see cref="Release"/>) after usage.
        /// </summary> 
        /// <param name="type">Type of the objects to resolve</param>
        /// <param name="argumentsAsAnonymousType">Constructor arguments</param>
        /// <returns>Object instances</returns>
        object[] ResolveAll(Type type, object argumentsAsAnonymousType);

        /// <summary>
        /// Releases a pre-resolved object. See Resolve methods.
        /// </summary>
        /// <param name="obj">Object to be released</param>
        void Release(object obj);

        /// <summary>
        /// Checks whether given type is registered before.
        /// </summary>
        /// <param name="type">Type to check</param>
        bool IsRegistered(Type type);

        /// <summary>
        /// Checks whether given type is registered before.
        /// </summary>
        /// <typeparam name="T">Type to check</typeparam>
        bool IsRegistered<T>();
    }
}
