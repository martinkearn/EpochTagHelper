using Microsoft.AspNet.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace epoch.TagHelpers
{
    public class EpochTagHelper : TagHelper
    {
        //Accepts any date formatter string i.e "dd MMM HH:mm:ss". Examples are here https://msdn.microsoft.com/en-us/library/zdtaw1bw(v=vs.110).aspx
        public string Formatter { get; set; }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            //Get child content (the content within the tags)
            var childContentRaw = (await output.GetChildContentAsync()).GetContent();

            //Check the child content is a double
            double childContent;
            bool isDouble = Double.TryParse(childContentRaw, out childContent);
            if (isDouble)
            {
                //Establish a base Epoch dateTime
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

                //calculate the passed in date in DateTime format
                var display = epoch.AddSeconds(Convert.ToDouble(childContent));

                //display output
                output.Content.SetContent(display.ToString(Formatter));
            }
            else {
                //if it is not a double, just display the raw child content
                output.Content.SetContent(childContentRaw);
            }

        }
    }
}
