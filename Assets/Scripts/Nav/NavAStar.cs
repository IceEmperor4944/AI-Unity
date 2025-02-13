using System.Collections.Generic;
using UnityEngine;
using Priority_Queue;
using UnityEngine.Rendering;

public class NavAStar
{
    public static bool Generate(NavNode start, NavNode end, ref List<NavNode> path)
    {
        var nodes = new SimplePriorityQueue<NavNode>();

        start.Cost = 0;
        float heuristic = (start.transform.position - end.transform.position).magnitude;
        nodes.Enqueue(start, start.Cost + heuristic);

        bool found = false;
        while (nodes.Count > 0 && !found)
        {
            var current = nodes.Dequeue();
            if (current == end)
            {
                found = true;
                break;
            }

            foreach (var neighbor in current.neighbors)
            {
                float cost = current.Cost * (current.transform.position - neighbor.transform.position).magnitude;
                if (cost < neighbor.Cost)
                {
                    neighbor.Cost = cost;
                    neighbor.PrevNode = current;

                    heuristic = (neighbor.transform.position - end.transform.position).magnitude;
                    nodes.Enqueue(neighbor, cost + heuristic);
                }
            }
        }

        if (found)
        {
            NavNode.CreatePath(end, ref path);
        }

        return found;
    }
}