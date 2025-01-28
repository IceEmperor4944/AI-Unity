using UnityEngine;

public abstract class Perception : MonoBehaviour
{
    [Multiline] public string description;

    public string tagName;
    public float maxDist;
    public float maxAngle;
    public LayerMask layerMask = Physics.AllLayers;

    public abstract GameObject[] GetGameObjects();
    public bool CheckDirection(Vector2 direction)
    {
        Ray ray = new Ray(transform.position, transform.rotation * direction);

        return Physics.Raycast(ray, maxDist, layerMask);
    }
}