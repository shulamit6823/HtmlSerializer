using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace פרקטיקוד_2
{
    public static class HtmlElementExtension
    {

        public static HashSet<HtmlElement> FindElement(
            this HtmlElement element,Selector selector)
        {
            var result=new HashSet<HtmlElement>();
            FindRecursive(element, selector, result);
            return result;
        }
        private static void FindRecursive(HtmlElement element, Selector selector,
            HashSet<HtmlElement> result )
        {
            var matches = element.descendants().Skip(1).
                Where(e => selector.IsMatch(e));

            if(selector.Child==null)
            {
                foreach (var match in matches)
                    result.Add(match);
                return;
            }
            foreach (var match in matches)  
                FindRecursive(match, selector.Child, result);
        }
    }
}
