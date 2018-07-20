using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Applications.Services.Dtos
{
    [Serializable]
    public class ComboboxItemDto
    {
        /// <summary>
        /// 项目值.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        ///显示文本信息.
        /// </summary>
        public string DisplayText { get; set; }

        /// <summary>
        /// 是否选中?
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public ComboboxItemDto()
        {

        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="value">Value of the item</param>
        /// <param name="displayText">Display text of the item</param>
        public ComboboxItemDto(string value, string displayText)
        {
            Value = value;
            DisplayText = displayText;
        }
    }
}
