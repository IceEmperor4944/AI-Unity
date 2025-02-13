using Unity.AI.Navigation;
using UnityEngine;

public class ChangeMaze : MonoBehaviour
{
    public GameObject[] WallsToRemove;
    public GameObject[] WallsToAdd;
    public NavMeshSurface[] navMeshSurface;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < WallsToRemove.Length; ++i)
            {
                WallsToRemove[i].SetActive(false);
            }
            for (int i = 0; i < WallsToAdd.Length; ++i)
            {
                WallsToAdd[i].SetActive(true);
            }
            for (int i = 0; i < navMeshSurface.Length; ++i)
            {
                navMeshSurface[i].UpdateNavMesh(navMeshSurface[i].navMeshData);
            }
            Destroy(gameObject);
        }
    }
}