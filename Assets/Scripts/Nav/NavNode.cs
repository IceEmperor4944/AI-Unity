using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class NavNode : MonoBehaviour
{
    public List<NavNode> neighbors = new List<NavNode>();

    public float Cost { get; set; } = float.PositiveInfinity;
    public NavNode PrevNode { get; set; } = null;

    public NavNode GetRandomNeighbor()
    {
        return (neighbors.Count > 0) ? neighbors[Random.Range(0, neighbors.Count)] : null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out NavPath path))
        {
            if (path.TargetNode == this)
            {
                path.TargetNode = path.GetNextNavNode(this);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out NavPath path))
        {
            if (path.TargetNode == this)
            {
                path.TargetNode = path.GetNextNavNode(this);
            }
        }
    }

    //public NavNode GetNearestNode()
    //{

    //}

    #region HELPER
    public static NavNode[] GetNavNodes()
    {
        return FindObjectsByType<NavNode>(FindObjectsSortMode.None);
    }

    public static NavNode[] GetNavNodes(string tag)
    {
        List<NavNode> result = new List<NavNode>();
        var gameObjects = GameObject.FindGameObjectsWithTag(tag);

        foreach (var g in gameObjects)
        {
            if (g.TryGetComponent(out NavNode node))
            {
                result.Add(node);
            }
        }

        return result.ToArray();
    }

    public static NavNode GetRandomNavNode()
    {
        var navNodes = GetNavNodes();
        return (navNodes == null) ? null : navNodes[Random.Range(0, navNodes.Length)];
    }

    /// <summary>
	/// Finds the nearest NavNode to a given position based on squared distance.
	/// </summary>
	public static NavNode GetNearestNavNode(Vector3 position)
    {
        NavNode nearestNode = null;
        float nearestDistance = float.MaxValue;

        var nodes = NavNode.GetNavNodes();
        foreach (var node in nodes)
        {
            float distance = (position - node.transform.position).sqrMagnitude; // Use sqrMagnitude for efficiency
            if (distance < nearestDistance)
            {
                nearestNode = node;
                nearestDistance = distance;
            }
        }

        return nearestNode;
    }

    /// <summary>
    /// Reconstructs the path from the given node back to the start node using the Previous references.
    /// </summary>
    public static void CreatePath(NavNode node, ref List<NavNode> path)
    {
        // Traverse backward through the previous nodes to reconstruct the shortest path
        while (node != null)
        {
            path.Add(node); // Add current node to the path
            node = node.PrevNode; // Move to the previous node in the path
        }

        // Reverse the path to ensure it follows the correct order (start to destination)
        path.Reverse();
    }

    /// <summary>
    /// Resets all NavNodes, clearing pathfinding data (Cost and Previous references).
    /// </summary>
    public static void ResetNodes()
    {
        var nodes = GetNavNodes();
        foreach (var node in nodes)
        {
            node.PrevNode = null;
            node.Cost = float.MaxValue; // Reset cost to a high value (infinity equivalent)
        }
    }
    #endregion
}