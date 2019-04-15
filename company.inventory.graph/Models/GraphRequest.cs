using Newtonsoft.Json.Linq;

namespace company.inventory.graph.Models
{
    public class GraphRequest
    {
        public string Query { get; set; }
        public JObject Variables { get; set; }
    }
}
