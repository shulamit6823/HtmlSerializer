using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace פרקטיקוד_2
{
    public class Selector
    {
        public string TagName { get; set; }
        public string Id { get; set; }
        public List<string> Classes { get; set; }=new List<string>();
        public Selector Parent { get; set; }
        public Selector Child { get; set; }

       
        public static Selector Parse(string query) 
        { 
            var parts=query.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Selector? root = null;
            Selector? current = null;

            foreach(var part in parts)
            {
                var selector = new Selector();
                var p = Regex.Split(part, "(?=[.#])");
                foreach(var c in p)
                {
                    if (string.IsNullOrEmpty(c))
                        continue;
                    if (c.StartsWith("#"))
                        selector.Id = c.Substring(1);
                    else if (c.StartsWith("."))
                        selector.Classes.Add(c.Substring(1));
                    else if (HtmlHelper.Instance.HtmlTags.Contains(c.ToLower()))
                        selector.TagName = c.ToLower();
                }

                if(root == null)
                    root = selector;
                else
                {
                    current.Child = selector;
                    selector.Parent = current;
                }
                current = selector;
            }
            return root;
        }
        public bool IsMatch(HtmlElement element)
        {
            if (TagName != null && element.Name != TagName)
                return false;
            if((Id != null && element.Id != Id)
                return false;
            return Classes.All(c=> element.classes.Contains(c));    
        }
        //public IEnumerable<HtmlElement> Query(HtmlElement root, string selector)
        //{
            
        //}
    }
}
