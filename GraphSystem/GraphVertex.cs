using System;
using System.Collections.Generic;

namespace WordTranslationGraph.GraphSystem
{
    public class GraphVertex
    {
        public GraphSide Side { get; }
        public string Content { get; }
        public List<GraphVertex> BindedVertices { get; } = new List<GraphVertex>();
        public GraphVertex (GraphSide graphSide, string content)
        {
            Side = graphSide;
            Content = content;
        }
        public GraphVertex BindVertex(GraphVertex vertex)
        {
            if (vertex == this)
                throw new Exception("Нельзя привязать вершину к вершине");
            
            if (vertex.Side == Side)
                throw new Exception($"Нельзя привязать вершину к той же стороне ({Side})");

            BindedVertices.Add(vertex);

            return this;
        }

        public GraphVertex UnbindVertex(GraphVertex vertex)
        {
            BindedVertices.Remove(vertex);
            
            return this;
        }

        public override string ToString()
        {
            return $"content {Content}, side {Side};";
        }
    }
}