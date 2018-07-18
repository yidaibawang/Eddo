using DoSoft.Admin.Models;
using DoSoft.Core.Security;
using Eddo.Web.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eddo.Extensions;
namespace DoSoft.Admin.Controllers
{
    public class WidgetController: EddoController
    {
        private readonly ModuleMange _moduleMange;
        public WidgetController(ModuleMange moduleMange)
        {
            _moduleMange = moduleMange;
        }
        [ChildActionOnly]
        public virtual ActionResult WidgetsByZone(string widgetZone)
        {
            
            return PartialView(widgetZone);
        }
        public virtual ActionResult Men()
        {
            int[] rootIds = _moduleMange.Modules.Where(m => m.ParentId == null).OrderBy(m => m.OrderCode).Select(m => m.Id).ToArray();
            var moduletree = GetModulesWithChecked(rootIds);
            return PartialView("Menu", moduletree);
        }
        private List<TreeNode> GetModulesWithChecked(int[] rootIds)
        {
            var modules = _moduleMange.Modules.Where(m => rootIds.Contains(m.Id)).OrderBy(m => m.OrderCode).Select(m => new
            {
                m.Id,
                m.Name,
                m.OrderCode,
                m.Remark,
                m.area,
                m.controller,
                m.action,
                m.IconClass,
                ChildIds = _moduleMange.Modules.Where(n => n.ParentId == m.Id).OrderBy(n => n.OrderCode).Select(n => n.Id).ToList()
            }).ToList();
            List<TreeNode> nodes = new List<TreeNode>();
            foreach (var item in modules)
            {
                TreeNode node = new TreeNode();
                node.Id = item.Id;
                node.Name = item.Name;
                node.IconCls = item.IconClass;
                if (item.controller.IsNullOrEmpty() || item.controller.IsNullOrEmpty())
                {
                    node.Url = null;
                }
                else
                {
                    node.Url = Url.Action(item.action,item.controller, new { area = item.area });
                }
                node.HasChildren = item.ChildIds.Count > 0 ? true : false;
                node.Items = item.ChildIds.Count > 0 ? GetModulesWithChecked(item.ChildIds.ToArray()) : new List<TreeNode>();
                nodes.Add(node);
            }
            return nodes;
        }
    }
}