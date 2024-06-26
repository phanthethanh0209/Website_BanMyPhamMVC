using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using System.Web.Mvc;

namespace WebsiteTheFaceShop.Areas.HtmlHelpers
{
    public static class InputHelper
    {
        public static MvcHtmlString InputHelp(this HtmlHelper htmlHelper,
            string CssClassName, string place, string Field, string Label, string value)
        {

            var outerDiv = new TagBuilder("div");
            outerDiv.AddCssClass(CssClassName);

            var label = new TagBuilder("label");
            label.MergeAttribute("for", Field);
            label.SetInnerText(Label);

            var input = new TagBuilder("input");
            input.AddCssClass("form-control");
            input.MergeAttribute("type", "text");
            input.MergeAttribute("id", Field);
            input.MergeAttribute("name", Field);
            input.MergeAttribute("placeholder", place);
            input.MergeAttribute("value", value);

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(label.ToString(TagRenderMode.Normal));
            stringBuilder.Append(input.ToString(TagRenderMode.SelfClosing));

            outerDiv.InnerHtml = stringBuilder.ToString();

            return MvcHtmlString.Create(outerDiv.ToString(TagRenderMode.Normal));

        }
    }
}