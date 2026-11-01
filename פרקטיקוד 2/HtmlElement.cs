using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace פרקטיקוד_2
{
    public class HtmlElement
    {
        public List<string> classes { get; set; }=new List<string>();
        public string Name { get; set; }
        public string Id { get; set; }
        public HtmlElement Parent { get; set; }
        public List<HtmlElement> Children { get; set; }=new();
        public string InnerHtml { get; set; } = "";
        public Dictionary<string, string> Attributes { get; set; } = new();


        public IEnumerable<HtmlElement> descendants()
        {
            var queue=new Queue<HtmlElement>();
            queue.Enqueue(this);
            while(queue.Count > 0)
            {
                var current=queue.Dequeue();
                yield return queue.Dequeue();

                foreach(var child in current.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }

        public IEnumerable<HtmlElement> Ancestors()
        {
            var current = this.Parent;
            while(current!=null)
            {
                yield return current;
                current=current.Parent;
            }
        }
    }
}
