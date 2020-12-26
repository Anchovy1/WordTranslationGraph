using System.Collections.Generic;
using System.Linq;
using WordTranslationGraph.GraphSystem;

namespace WordTranslationGraph
{
    public class WordTranslation
    {
        private Graph _graph;
        public WordTranslation()
        {
            _graph = new Graph();
        }

        public void AddWord(string word, string translation, bool isEnglishWord = false)
        {
            var wordSide = isEnglishWord ? GraphSide.Right : GraphSide.Left;
            var word2Side = isEnglishWord ? GraphSide.Left : GraphSide.Right;

            _graph.BindVertexes(_graph.AddVertex(new GraphVertex(wordSide, word)), 
                _graph.AddVertex(new GraphVertex(word2Side, translation)));
        }

        public void RemoveWord(string word, bool isRussianWord = false)
        {
            var wordSide = isRussianWord ? GraphSide.Right : GraphSide.Left;
            var wordVertexes = _graph.FindVertex(wordSide, word);
            if (wordVertexes.Count != 0)
                _graph.RemoveVertex(wordVertexes.First());
        }

        public List<string> SeekWordTranslation(string word, bool isRussianWord = false)
        {
            var seekSide = isRussianWord ? GraphSide.Right : GraphSide.Left;
            var words = new List<string>();

            // ищем вершины, где есть слово
            var vertexes = _graph.FindVertex(seekSide, word);
            foreach (var vertex in vertexes) {
                foreach (var bindedVertex in vertex.BindedVertices) {
                    words.Add(bindedVertex.Content);
                }
            }

            return words;
        }

        public override string ToString()
        {
            return _graph.ToString();
        }
        
        public string GetJson()
        {
            var exporter = new GraphExporter(_graph);
            return exporter.ToString();
        }
    }
}