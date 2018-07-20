using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Eddo.Extensions;
namespace Eddo.Web.Mvc.Extensions
{
    public static class BootstrapHtmlExtensions
    {
        public static MvcHtmlString EddoLabelFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, bool displayHint = true)
        {
            var result = new StringBuilder();
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            var hintResource = string.Empty;

            result.Append(helper.LabelFor(expression, new { title = hintResource, @class = "control-label" }));
            if (metadata.DisplayName.IsNullOrEmpty())
            {
                result.Append(helper.Hint(hintResource).ToHtmlString());
            }
            var laberWrapper = new TagBuilder("div");
            laberWrapper.Attributes.Add("class", "label-wrapper");
            laberWrapper.InnerHtml = result.ToString();

            return MvcHtmlString.Create(laberWrapper.ToString());
        }
        public static MvcHtmlString Hint(this HtmlHelper helper, string value)
        {
            //create tag builder
            var builder = new TagBuilder("div");
            builder.MergeAttribute("title", value);
            builder.MergeAttribute("class", "ico-help");
            var icon = new StringBuilder();
            icon.Append("<i class='fa fa-question-circle'></i>");
            builder.InnerHtml = icon.ToString();
            //render tag
            return MvcHtmlString.Create(builder.ToString());
        }
    }
}
