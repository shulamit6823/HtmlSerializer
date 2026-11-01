
using פרקטיקוד_2;

var serializer = new HtmlSerializer();
var html = await serializer.Load("https://example.com");
var dom=serializer.Serialize(html);

var selector = Selector.Parse("div p");
var result=dom.FindElement(selector);

Console.WriteLine($"Found {result.Count} elements");

foreach (var element in result)
{
    Console.WriteLine($"<{element.Name}> id={element.Id} " +
        $"classes={string.Join(",",element.classes)}");
}