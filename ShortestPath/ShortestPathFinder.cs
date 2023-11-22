// See https://aka.ms/new-console-template for more information
using Models;
using System;


public class ShortestPathFinder
{
    public ShortestPathData ShortestPath(string fromNodeName, string toNodeName, List<Nodes> graphNodes)
    {
        // Validate input
        if (graphNodes == null || string.IsNullOrEmpty(fromNodeName) || string.IsNullOrEmpty(toNodeName))
        {
            throw new ArgumentException("Invalid input parameters");
        }

        // Initialize data structures
        Dictionary<string, int> distance = new Dictionary<string, int>();
        Dictionary<string, string> previousNode = new Dictionary<string, string>();
        HashSet<string> visited = new HashSet<string>();

        foreach (var node in graphNodes)
        {
            distance[node.Name] = int.MaxValue;
            previousNode[node.Name] = null;
        }

        distance[fromNodeName] = 0;

        // Dijkstra's Algorithm
        while (visited.Count < graphNodes.Count)
        {
            string currentNode = GetMinDistanceNode(distance, visited);
            visited.Add(currentNode);

            foreach (var neighbor in graphNodes.Find(n => n.Name == currentNode).Neighbors)
            {
                if (!visited.Contains(neighbor.Key.Name))
                {
                    int newDistance = distance[currentNode] + neighbor.Value;

                    if (newDistance < distance[neighbor.Key.Name])
                    {
                        distance[neighbor.Key.Name] = newDistance;
                        previousNode[neighbor.Key.Name] = currentNode;
                    }
                }
            }
        }

        // Reconstruct the path
        List<string> path = new List<string>();
        string current = toNodeName;
        while (current != null)
        {
            path.Add(current);
            current = previousNode[current];
        }
        path.Reverse();

        // Return the result
        return new ShortestPathData
        {
            NodeNames = path,
            Distance = distance[toNodeName]
        };
    }

    private string GetMinDistanceNode(Dictionary<string, int> distance, HashSet<string> visited)
    {
        int minDistance = int.MaxValue;
        string minNode = null;

        foreach (var entry in distance)
        {
            if (!visited.Contains(entry.Key) && entry.Value < minDistance)
            {
                minDistance = entry.Value;
                minNode = entry.Key;
            }
        }

        return minNode;
    }

    static void Main()
    {
        // Create nodes and edges
        List<Nodes> graphNodes = new List<Nodes>
        {
            new Nodes("A"),
            new Nodes("B"),
            new Nodes("C"),
            new Nodes("D"),
            new Nodes("E"),
        };

        graphNodes[0].Neighbors.Add(graphNodes[1], 2);
        graphNodes[0].Neighbors.Add(graphNodes[2], 4);
        graphNodes[1].Neighbors.Add(graphNodes[2], 1);
        graphNodes[1].Neighbors.Add(graphNodes[3], 7);
        graphNodes[2].Neighbors.Add(graphNodes[4], 3);
        graphNodes[3].Neighbors.Add(graphNodes[4], 1);

        // Find the shortest path
        ShortestPathFinder shortestPath = new ShortestPathFinder();
        var result = shortestPath.ShortestPath("A", "D", graphNodes);

        // Print the result
        Console.WriteLine("Shortest Path: " + string.Join(", ", result.NodeNames));
        Console.WriteLine("Total Distance: " + result.Distance);
    }

}
