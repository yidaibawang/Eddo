using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Web.Mvc.Models
{
    public class TreeNode
    {
        /// <summary>
        /// 初始化一个<see cref="TreeNode"/>类型的新实例
        /// </summary>
        public TreeNode()
        {
            Items = new List<TreeNode>();
        }

        /// <summary>
        /// 获取或设置 编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 获取或设置 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 是否选中
        /// </summary>
        public bool IsChecked { get; set; }

        /// <summary>
        /// 获取或设置 排序码
        /// </summary>
        public double OrderCode { get; set; }

        /// <summary>
        /// 获取或设置 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 获取或设置 图标样式
        /// </summary>
        public string IconCls { get; set; }

        /// <summary>
        /// 获取或设置 URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 获取或设置 是否有子项
        /// </summary>
        public bool HasChildren { get; set; }

        /// <summary>
        /// 获取或设置 附加数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 获取或设置 子项集合
        /// </summary>
        public List<TreeNode> Items { get; set; }
    }
}
