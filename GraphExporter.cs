using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using WordTranslationGraph.GraphSystem;

namespace WordTranslationGraph
{
    public class GraphInfo
    {
        public GraphSide Side { get; set; }
        public string Content { get; set; }
        public List<GraphInfo> BindendInfos { get; set; }
    }
    public class GraphExporter
    {
        private List<GraphInfo> _graphObjects;
        public GraphExporter(Graph graph)
        {
            _graphObjects = graph.FindVertex(GraphSide.Left, null).Select(vertex => new GraphInfo
            {
                Side = vertex.Side,
                Content = vertex.Content,
                BindendInfos = vertex.BindedVertices.Select(vertexIn => new GraphInfo
                {
                    Side = vertexIn.Side,
                    Content = vertexIn.Content,
                }).ToList()
            }).ToList();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(_graphObjects, Formatting.Indented, 
                new JsonSerializerSettings 
                { 
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
        }
        
    }
}