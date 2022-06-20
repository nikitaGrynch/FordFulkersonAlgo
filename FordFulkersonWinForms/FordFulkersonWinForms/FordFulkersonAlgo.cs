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
        private Vertex[] StartGraph;  // начальный граф
        private Vertex[] vertices;  // вершины графа
        private ResVertex[] FiniteGraph;  // конечный граф
        public int sourceVertex;    // исток
        public int targetVertex;    // сток
        public int VertexCount;     // кол-во вершин
        public String resultStr = String.Empty;

        public int GetMaxGraphFlow()
        {
            resultStr = String.Empty;
            List<Route> routes = new List<Route>();                 // список всех маршрутов

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
                        if(v == sourceVertex)                       // если мы еще в истоке, то заканчиваем работу алгоритма
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
                    
                    if(j == targetVertex)                           // если дошли до стока
                    {
                        int maxFlow = GetMaxRouteFlow(marks);       // ищем максимальную пропускную способность в маршруте

                        routes.Add(new Route                        // добовляем маршрут в список всех марщрутов
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
                        resultStr += routes.Last();
                        Update(marks, maxFlow);
                        break;
                    }

                    v = j;

                }
            }
            int res = 0;
            foreach(var route in routes)
            {
                res += route.MaxFlow;
            }
            resultStr += "\nMax network flow: " + res;
            BuildFiniteGraph();
            return res;
        }

        private void Update(List<VertexMark> marks, int maxRouteFlow)       // обновляем пропускнуые способности для потоков
        {
            foreach (var mark in marks)                 // проходим по всем меткам
            {
                int startVertex = mark.From;            // начальная вершина потока
                int endVertex = mark.CurrentVertex;     // конечная вершина потока
                int i = 0;
                foreach (var vertex in vertices)
                {
                    if(vertex.VertexNumber == startVertex)
                    {
                        foreach(var v in vertex.Connections)
                        {
                            if (v.VertexNumber == endVertex)
                            {
                                vertices[i].Connections.Where(ver => ver.VertexNumber == v.VertexNumber).First().CapacityTo -= maxRouteFlow;
                                vertices[i].Connections.Where(ver => ver.VertexNumber == v.VertexNumber).First().CapacityFrom += maxRouteFlow;
                            }
                        }
                    }
                    
                    else if(vertex.VertexNumber == endVertex)
                    {
                        foreach (var v in vertex.Connections)
                        {
                            if (v.VertexNumber == startVertex)
                            {
                                vertices[i].Connections.Where(ver => ver.VertexNumber == v.VertexNumber).First().CapacityTo += maxRouteFlow;
                                vertices[i].Connections.Where(ver => ver.VertexNumber == v.VertexNumber).First().CapacityFrom -= maxRouteFlow;
                            }
                        }
                    }
                    
                    i++;
                }
            }
        }


        private int GetMaxVertex(int currentVertex, List<int> usedVertices)     // выбираем вершину, максимальный поток пути к которой будет наибольшим
        {
            if (currentVertex == targetVertex)
                return -1;
            var Vs = vertices[currentVertex - 1].Connections.OrderByDescending(v => v.CapacityTo);     // сортируем доступные вершины по пропускной спобности
            foreach(var V in Vs)
            {
                if (usedVertices.Contains(V.VertexNumber))      // если вершина уже была рассмотрена ранее
                    continue;
                if(V.CapacityTo > 0)
                    return V.VertexNumber;
            }
            return -1;
        }

        private int GetMaxRouteFlow(List<VertexMark> marks)     // получение максимального потока маршрута
        {
            return marks.OrderBy(m => m.Capacity).First().Capacity;

        }

        private void BuildFiniteGraph()             // построение конечного графа
        {
            FiniteGraph = new ResVertex[VertexCount];       // конечный граф
            int i = 0;
            foreach(var startVertex in StartGraph)          // проходимся по всему начальному графу
            {
                FiniteGraph[i].VertexNumber = StartGraph[i].VertexNumber;   // записываем номер вершины в конечный граф
                FiniteGraph[i].Connections = new List<ResConnectedVertex>();
                int j = 0;
                foreach (var connectedVertex in startVertex.Connections)    // проходимся по всем связям начального графа
                {
                    if (vertices[i].Connections[j].CapacityFrom - StartGraph[i].Connections[j].CapacityFrom > 0)    // проверяем, что направление правильное
                    {
                        FiniteGraph[i].Connections.Add(new ResConnectedVertex   // добавляем новую вуршину в связанные вершины графа
                        {
                            VertexNumber = connectedVertex.VertexNumber,
                            Capacity = vertices[i].Connections[j].CapacityFrom - StartGraph[i].Connections[j].CapacityFrom
                        });
                    }
                    j++;
                }
                i++;
            }
            using (var jsonFile = new StreamWriter("FiniteGraph.json"))
            {
                jsonFile.Write(JsonSerializer.Serialize<ResVertex[]>(FiniteGraph));
            }
        }

        public void GetData()   // получение входных данных: граф, исток, сток
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

        public void GetDataFromJson(String json)
        {
            try
            {
                StartGraph = JsonSerializer.Deserialize<Vertex[]>(json);
                if (StartGraph == null)
                    throw new Exception("IncorrectFile");
                VertexCount = StartGraph.Count();
                vertices = JsonSerializer.Deserialize<Vertex[]>(json);
                if (vertices == null)
                    throw new Exception("IncorrectFile");
            }
            catch
            {
                throw (new Exception("IncorrectFile"));
            }
        }

        class ConnectedVertex                      // вершина, которая связана с рассматриваемой вершиной
        {
            public int VertexNumber { get; set; }   // номер вершины
            public int CapacityFrom { get; set; }   // пропускная способность от вершины
            public int CapacityTo { get; set; }     // пропускная способность к вершине

        }

        class ResConnectedVertex                      // вершина, которая связана с рассматриваемой вершиной
        {
            public int VertexNumber { get; set; }   // номер вершины
            public int Capacity { get; set; }       // пропускная способность

        }

        struct Vertex
        {
            public int VertexNumber { get; set; }
            public List<ConnectedVertex> Connections { get; set; } // отношения с другими вершинами
        }
        struct ResVertex
        {
            public int VertexNumber { get; set; }
            public List<ResConnectedVertex> Connections { get; set; } // отношения с другими вершинами
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
                int max = Vertices.Count();
                int i = 1;
                foreach (var vertex in Vertices)
                {
                    if (i == max)
                    {
                        res += vertex;
                    }
                    else
                    {
                        res += vertex + " --> ";
                    }

                    i++;
                }
                res += "\nMax route flow: " + MaxFlow + "\n\n";
                return res;

            }

        }
    }
}
