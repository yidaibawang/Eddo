using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eddo.Permissions.Dtos
{
    public class EddoModuleInputDtoBase<TModuleKey> where TModuleKey: struct, IEquatable<TModuleKey>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public TModuleKey Id { get; set; }

        /// <summary>
        /// 获取或设置 模块名称
        /// </summary>
        [Required, DisplayName("模块名称")]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 备注
        /// </summary>
        [DisplayName("模块描述")]
        public string Remark { get; set; }

        /// <summary>
        /// 获取或设置 模块代码
        /// </summary>
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// 获取或设置 节点内排序码
        /// </summary>
        [DisplayName("排序码")]
        public double OrderCode { get; set; }

        /// <summary>
        /// 获取或设置 父模块编号
        /// </summary>
        public TModuleKey? ParentId { get; set; }
    }
}
