using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lab3
{
    public class DijkstraAlgorithm
    {
        private List<(int node, float weight)>[] adjacencyList;
        private float[] distances;
        private int[] previousNodes;
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
            previousNodes = new int[n];
            visited = new bool[n];
        }

        public void Run()
        {
            for (int i = 0; i < adjacencyList.Length - 1; i++)
            {
                int currentNode = GetClosestUnvisitedNode();
                visited[currentNode] = true;

                foreach ((int neighbor, float weight) in adjacencyList[currentNode])
                {
                    float distanceThroughCurrentNode = distances[currentNode] + weight;

                    if (distanceThroughCurrentNode < distances[neighbor])
                    {
                        distances[neighbor] = distanceThroughCurrentNode;
                        previousNodes[neighbor] = currentNode;
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
        public List<int>[] GetShortestPaths()
        {
            List<int>[] shortestPaths = new List<int>[adjacencyList.Length];

            for (int i = 0; i < adjacencyList.Length; i++)
            {
                shortestPaths[i] = GetShortestPathTo(i);
            }

            return shortestPaths;
        }

        private List<int> GetShortestPathTo(int node)
        {
            List<int> path = new List<int>();
            int currentNode = node;

            while (currentNode != sourceNode)
            {
                path.Add(currentNode + 1);
                currentNode = previousNodes[currentNode];
            }

            path.Add(sourceNode + 1);
            path.Reverse();

            return path;
        }
    }
}
