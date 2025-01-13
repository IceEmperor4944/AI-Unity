using UnityEngine;

public abstract class Perception : MonoBehaviour
{
    public string tagName;
    public float maxDist;
    public float maxAngle;

    public abstract GameObject[] GetGameObjects();
}