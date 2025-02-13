using UnityEngine;
using Unity.AI.Navigation;
using System.Collections;

[RequireComponent(typeof(NavMeshSurface))]
public class NewMonoBehaviourScript : MonoBehaviour
{
    private NavMeshSurface navMeshSurface;
    public GameObject[] characters;
    public Transform[] spawnPoints;

    private void Awake()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
    }

    void Start()
    {
        navMeshSurface.BuildNavMesh();
        StartCoroutine(SpawnCharacters());
    }

    IEnumerator SpawnCharacters()
    {
        yield return new WaitForEndOfFrame();

        for(int i = 0; i < characters.Length; i++)
        {
            Instantiate(characters[i], spawnPoints[i].position, spawnPoints[i].rotation);
        }
    }
}