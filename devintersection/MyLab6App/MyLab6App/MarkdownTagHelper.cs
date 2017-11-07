using CommonMark;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLab6App
{
    [HtmlTargetElement("p", Attributes = "markdown")]
    public class MarkdownTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var content = await output.GetChildContentAsync();
            var markdown = content.GetContent();
            var result = CommonMarkConverter.Convert(markdown);
            output.Content.SetHtmlContent(CommonMarkConverter.Convert(markdown));
            output.TagName = null;
        }
    }
}