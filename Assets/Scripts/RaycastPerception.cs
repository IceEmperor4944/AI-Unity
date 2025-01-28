using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class RaycastPerception : Perception
{
    [SerializeField] int numRaycast = 2;

    public override GameObject[] GetGameObjects()
    {
        List<GameObject> result = new List<GameObject>();

        return result.ToArray();
    }
}