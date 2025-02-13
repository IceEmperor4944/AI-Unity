using Unity.AI.Navigation;
using UnityEngine;

public class StepsScript : MonoBehaviour
{
    private NavMeshSurface[] stepsSurface;
    public bool haveAccess = false;

    void Start()
    {
        stepsSurface = GetComponents<NavMeshSurface>();
        stepsSurface[1].enabled = false;
    }

    void Update()
    {
        if (haveAccess)
        {
            if (!stepsSurface[1].isActiveAndEnabled)
            {
                stepsSurface[1].enabled = true;
            }
        }
    }
}