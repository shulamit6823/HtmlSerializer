using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace פרקטיקוד_2
{
    internal class HtmlSerializer
    {
        public async Task<string> Load(string url)
        {
            HttpClient client = new HttpClient();
            var responce=await client.GetAsync(url);
            return await responce.Content.ReadAsStringAsync();    
        }

        public List<string> SplitToParts(string html)
        {
            var cleanHtml=new Regex("\\s+").Replace(html, " ");
            var newHtml=new Regex("(<.*?>)").Split(cleanHtml).Select(p=>p.Trim()).Where(p=>p!="").ToList();
            return newHtml;
        }

        public HtmlElement Serialize(string html)
        {
            List<string> parts= SplitToParts(html);
            var root = new HtmlElement { Name = "root" };
            var current = root;
            foreach (var item in parts)
            {
                if (!item.StartsWith("<"))
                {
                    current.InnerHtml += item;
                    continue;
                }
                if(item.StartsWith("<!"))
                {
                    continue;
                }
                var content = item.Substring(1, item.Length - 2).Trim();
                var firstWord = content.Split(' ')[0].ToLower();
                if (firstWord == "/html")
                    break;
                if (firstWord.StartsWith("/"))
                {
                    if(current.Parent!=null)
                        current = current.Parent;
                    continue;
                }
                var tagName = firstWord.TrimEnd('/');
                if(!HtmlHelper.Instance.HtmlTags.Contains(tagName))
                {
                    current.InnerHtml += item;
                    continue;
                }
                var element = new HtmlElement
                    { Name = tagName, Parent=current };
                 
                current.Children.Add(element);
                ParseAttributes(element,content.Substring(firstWord.Length));

                bool isSelfClosing=content.EndsWith("/")||
                    HtmlHelper.Instance.HtmlVoidTags.Contains(tagName);
                if (!isSelfClosing)
                    current = element;
               
            }
            return root;
        }
        void ParseAttributes(HtmlElement element, string attribute)
        {
            var regex = new Regex
           ("(?<name>[\\w:-]+)\\s*=\\s*(?:\"(?<value>.*?)\"|'(?<value>.*?)')");

            foreach (Match match in regex.Matches(attribute))
            {
                var name = match.Groups["name"].Value.ToLower();
                var value = match.Groups["value"].Value;
                element.Attributes[name] = value;

                if (name == "id")
                    element.Id = value;
                if (name == "class")
                    element.classes = value.Split(' ',
                        StringSplitOptions.RemoveEmptyEntries).ToList();
            }
        }
    }
}
