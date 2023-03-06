using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows;

namespace Lab2
{


    public class DijkstraAlgorithm
    {
        private List<(int node, float weight)>[] adjacencyList;
        private float[] distances;
        private bool[] visited;
        private int sourceNode;

        public DijkstraAlgorithm(List<(int node, float weight)>[] adjacencyList, int sourceNode)
        {
            this.adjacencyList = adjacencyList;
            this.sourceNode = sourceNode;

            int n = adjacencyList.Length;
            distances = new float[n];
            for (int i = 0; i < n; i++)
            {
                distances[i] = int.MaxValue;
            }
            distances[sourceNode] = 0;

            visited = new bool[n];
        }

        public void Run()
        {
            for (int i = 0; i < adjacencyList.Length - 1; i++)
            {
                int currentNode = GetClosestUnvisitedNode();
                visited[currentNode] = true;

                foreach ((int neighbor, float edgeWeight) in adjacencyList[currentNode])
                {
                     float distanceThroughCurrentNode = distances[currentNode] + edgeWeight;

                    if (distanceThroughCurrentNode < distances[neighbor])
                    {
                        distances[neighbor] = distanceThroughCurrentNode;
                    }
                }
            }
        }

        private int GetClosestUnvisitedNode()
        {
            int closestUnvisitedNode = -1;
            float shortestDistance = int.MaxValue;

            for (int i = 0; i < adjacencyList.Length; i++)
            {
                if (!visited[i] && distances[i] < shortestDistance)
                {
                    closestUnvisitedNode = i;
                    shortestDistance = distances[i];
                }
            }

            return closestUnvisitedNode;
        }

        public float[] GetShortestDistances()
        {
            return distances;
        }
    }
    static class Program
    {
        public static void Main(string[] args)
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormGraph());   
        }

      
    }

}