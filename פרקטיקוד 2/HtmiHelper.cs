using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace פרקטיקוד_2
{
    internal class HtmlHelper
    {
        private static readonly HtmlHelper _instance;
        public static HtmlHelper Instance => _instance;

        public string[] HtmlTags { get; set; }
        public string[] HtmlVoidTags { get; set; }
        private HtmlHelper()
        {
            HtmlTags =JsonSerializer.Deserialize<string[]>(File.ReadAllText("Seed/HtmlTags.json"));
            HtmlVoidTags = JsonSerializer.Deserialize<string[]>(File.ReadAllText("Seed/HtmlVoidTags.json"));

        }
    }
}
