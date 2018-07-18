using Eddo.Web.Mvc.Models;
using Eddo.Web.Mvc.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Eddo.Dependency;
namespace Eddo.Web.Mvc.Extensions
{
    public static  class LayoutExtensions
    {
      
     
        public static void SetActiveMenuItemSystemName(this HtmlHelper html, string systemName)
        {
            var pageHeadBuilder = EngineContext.Current.Resolve<IPageHeadBuilder>();
            pageHeadBuilder.SetActiveMenuItemSystemName(systemName);
        }
        /// <summary>
        /// Get system name of admin menu item that should be selected (expanded)
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <returns>System name</returns>
        public static string GetActiveMenuItemSystemName(this HtmlHelper html)
        {
            var pageHeadBuilder = EngineContext.Current.Resolve<IPageHeadBuilder>();
            return pageHeadBuilder.GetActiveMenuItemSystemName();
        }
        public static bool ContainsSystemName(this TreeNode node, string systemName)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            if (String.IsNullOrWhiteSpace(systemName))
                return false;

            if (systemName.Equals(node.Name, StringComparison.InvariantCultureIgnoreCase))
                return true;

            return node.Items.Any(cn => ContainsSystemName(cn, systemName));
        }
    }
}
