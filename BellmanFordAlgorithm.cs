using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2
{
    public class BellmanFordAlgorithm
    {
        private List<(int node, float weight)>[] adjacencyList;
        private float[] distances;
        private int sourceNode;
        int[] predecessors;
        public BellmanFordAlgorithm(List<(int node, float weight)>[] adjacencyList, int sourceNode)
        {
            this.adjacencyList = adjacencyList;
            this.sourceNode = sourceNode;
            distances = new float[adjacencyList.Length];
            predecessors = new int[adjacencyList.Length];

            for (int i = 0; i < adjacencyList.Length; i++)
            {
                distances[i] = int.MaxValue;
            }
            distances[sourceNode] = 0;



        }
        public void Run()
        {

            for (int i = 0; i < adjacencyList.Length - 1; i++)
            {
                for (int j = 0; j < adjacencyList.Length; j++)
                {
                    foreach ((int neighbor, float weight) in adjacencyList[j])
                    {
                        if (distances[j] + weight < distances[neighbor])
                        {
                            distances[neighbor] = distances[j] + weight;
                            predecessors[neighbor] = j;
                        }
                    }
                }
            }
        }
        public bool IsNegativeCycle()
        {
            
            distances[sourceNode] = 0f;
            
            
                for (int j = 0; j < adjacencyList.Length; j++)
                {
                    foreach ((int neighbor, float weight) in adjacencyList[j])
                    {
                        if ((distances[j] != int.MaxValue) && ((distances[j] + weight) < distances[neighbor] - 0.0001))
                        {
                            return true;
                        }
                    }
                }
            

            return false;
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
                currentNode = predecessors[currentNode];
            }

            path.Add(sourceNode + 1);
            path.Reverse();

            return path;
        }
        public float[] GetShortestDistances()
        {
            return distances;
        }
    }
}
