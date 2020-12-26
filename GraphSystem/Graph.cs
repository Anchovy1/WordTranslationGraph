using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace WordTranslationGraph.GraphSystem
{
    public class Graph
    {
        private List<GraphVertex> _leftSide = new List<GraphVertex>();
        private List<GraphVertex> _rightSide  = new List<GraphVertex>();

        /// <summary>
        /// Добавляет вершину в граф
        /// </summary>
        /// <param name="vertex">Вершина, которую надо добавить</param>
        /// <returns>Добавленная вершина</returns>
        public GraphVertex AddVertex(GraphVertex vertex)
        {
            var check = FindVertex(vertex.Side, vertex.Content);
            if (check.Count > 0)
                return check.First();
            
            var collection = vertex.Side == GraphSide.Left 
                ? _leftSide : _rightSide;
            collection.Add(vertex);

            return vertex;
        }

        /// <summary>
        /// Удаляет вершину из графа
        /// </summary>
        /// <param name="vertex">Вершина</param>
        /// <returns>Удаленная вершина</returns>
        public GraphVertex RemoveVertex(GraphVertex vertex)
        {
            if (vertex.Side == GraphSide.Left)
                _leftSide.Remove(vertex);
            else if (vertex.Side == GraphSide.Right)
                _rightSide.Remove(vertex);

            return vertex;
        }

        /// <summary>
        /// Отсоединить две вершины в графе 
        /// </summary>
        public void UnbindVertexes(GraphVertex vertex1, GraphVertex vertex2)
        {
            vertex1.UnbindVertex(vertex2);
            vertex2.UnbindVertex(vertex1);
        }
        
        /// <summary>
        /// Соединить две вершины в графе 
        /// </summary>
        public void BindVertexes(GraphVertex vertex1, GraphVertex vertex2)
        {
            vertex1.BindVertex(vertex2);
            vertex2.BindVertex(vertex1);
        }

        /// <summary>
        /// Найти вершины в графе по указанным данным
        /// </summary>
        /// <param name="side">Сторона (можно не указывать)</param>
        /// <param name="content">Слово (можно не указывать)</param>
        /// <returns>Список вершин, подлежащих условиям поиска.</returns>
        public List<GraphVertex> FindVertex(GraphSide? side, string content)
        {
            // обьединим все в одну кучу, чтобы была возможность вывести все вершины сразу.
            var vertexies = _leftSide.Concat(_rightSide);

            // если сторона указана, ищем по ней
            if (side != null)
                vertexies = vertexies.Where(vertex => vertex.Side == side);
            
            // если контент указан, ищем по нему
            if (!string.IsNullOrEmpty(content))
                vertexies = vertexies.Where(vertex => vertex.Content == content);
            
            return vertexies.ToList();
        }

        public override string ToString()
        {
            var sb = new StringBuilder ();
            foreach (var vertex in _leftSide)
                sb.Append($"{vertex.Content}, {vertex.Side}\n");
            foreach (var vertex in _rightSide)
                sb.Append($"{vertex.Content}, {vertex.Side}\n");

            return sb.ToString();
        }
    }
}