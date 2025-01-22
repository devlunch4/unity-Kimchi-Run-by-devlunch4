using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public float minSpawnDelay;
    public float maxSpawnDelay;

    [Header("References")]
    public GameObject[] gameObjects;

    // xxx Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start() { }

    void OnEnable()
    {
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));
    }

    // void Update()
    // {
    // }

    void Spawn()
    {
        var randomObject = gameObjects[Random.Range(0, gameObjects.Length)];
        Instantiate(randomObject, transform.position, Quaternion.identity);
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));
    }
}
