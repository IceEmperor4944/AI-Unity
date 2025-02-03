using UnityEngine;
using System.Collections.Generic;

public class NavNode : MonoBehaviour
{
    public List<NavNode> neighbors = new List<NavNode>();

    public NavNode GetRandomNeighbor()
    {
        return (neighbors.Count > 0) ? neighbors[Random.Range(0, neighbors.Count)] : null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out NavAgent agent))
        {

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

        foreach(var g in gameObjects)
        {
            if(g.TryGetComponent(out NavNode node))
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
    #endregion
}