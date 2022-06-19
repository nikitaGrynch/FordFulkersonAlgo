using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace FordFulkersonAlgo
{
    internal class FordFulkersonAlgo
    {
        private Vertex[] vertices;
        private int sourceVertex;
        private int targetVertex;

        public int GetMaxGraphFlow()
        {
            List<Route> routes = new List<Route>();

            int j = sourceVertex;
            while (j != -1)
            {
                int v = sourceVertex;                               // стартовая вершина
                List<int> usedVertices = new List<int>();           // использованные вершины
                usedVertices.Add(v);
                List<VertexMark> marks = new List<VertexMark>();    // метки маршрута

                while (v != targetVertex)                            // выполняем пока не дошли до стока
                {
                    j = GetMaxVertex(v, usedVertices);
                    if(j == -1)                                     // если не удалось выбрать следующую вершину
                    {
                        if(v == targetVertex)                       // если мы еще в истоке, то заканчиваем работу алгоритма
                        {
                            break;
                        }
                        usedVertices.Add(v);                        // добавляем вершину в просмотренную, чтобы ее дальше не рассматривать
                        v = marks.Last().From;                      // переходим на пердыдущую вершину
                        marks.Remove(marks.Last());                 // удаляем метку вершины
                        continue;
                    }

                    marks.Add(new VertexMark                        // добавляем метку маршрута
                    {   
                        From = v,
                        CurrentVertex = j,
                        Capacity = vertices[j - 1].Connections.Where(k => k.VertexNumber == v).First().CapacityFrom
                    });
                    usedVertices.Add(v);                            // добавляем вершину в просмотренную, чтобы ее дальше не рассматривать
                    if(j == targetVertex)
                    {
                        int maxFlow = GetMaxRouteFlow(marks);                     // ищем максимальную пропускную способность в маршруте

                        routes.Add(new Route
                        {
                            MaxFlow = maxFlow,
                            Vertices = new List<int>()
                        });
                        routes.Last().Vertices.Add(sourceVertex);
                        foreach(var mark in marks)
                        {
                            routes.Last().Vertices.Add(mark.CurrentVertex);
                        }
                        Console.WriteLine(routes.Last());
                        break;
                    }

                    v = j;

                }
            }
            return 0;
        }




        private int GetMaxVertex(int currentVertex, List<int> usedVertices)
        {
            if (currentVertex == targetVertex)
                return -1;
            var Vs = vertices[currentVertex - 1].Connections.OrderByDescending(v => v.CapacityTo);     // сортируем доступные вершины по пропускной спобности
            foreach(var V in Vs)
            {
                if (usedVertices.Contains(V.VertexNumber))
                    continue;
                return V.VertexNumber;
            }
            return -1;
        }

        private int GetMaxRouteFlow(List<VertexMark> marks)
        {
            return marks.OrderBy(m => m.Capacity).First().Capacity;

        }

        public void GetData()
        {

            using (var jsonFile = new StreamReader("ser.json"))
            {
                vertices = JsonSerializer.Deserialize<Vertex[]>(jsonFile.ReadToEnd());
            }
            Console.Write("Source vertex: ");
            sourceVertex = Convert.ToInt32(Console.ReadLine());

            Console.Write("Target vertex: ");
            targetVertex = Convert.ToInt32(Console.ReadLine());
        }
        struct ConnectedVertex                      // вершина, которая связана с рассматриваемой вершиной
        {
            public int VertexNumber { get; set; }   // номер вершины
            public int CapacityFrom { get; set; }   // пропускная способность от вершины
            public int CapacityTo { get; set; }     // пропускная способность к вершине

        }

        struct Vertex
        {
            public int VertexNumber { get; set; }
            public ConnectedVertex[] Connections { get; set; }  // отношения с другими вершинами
        }
        struct VertexMark                           // метка маршрута
        {
            public int Capacity { get; set; }       // пропускная способность
            public int From { get; set; }           // предыдущая вершина
            public int CurrentVertex { get; set; }  // текущая вершина
        }

        struct Route                                // Маршрут
        {
            public List<int> Vertices { get; set; } // вершины маршрута
            public int MaxFlow { get; set; }        // максимальный поток маршрута

            public override string ToString()
            {
                String res = String.Empty;
                foreach (var vertex in Vertices)
                {
                    res += vertex + " --> ";
                }
                res += MaxFlow;
                return res;
            }

        }
    }
}
