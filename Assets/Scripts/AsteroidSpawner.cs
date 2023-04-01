using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] GameObject prefab;
    [SerializeField] float trayectoryVariance = 15.0f;
    [SerializeField] float spawnRate = 2.0f;
    [SerializeField] float spawnDistance = 15.0f;
    [SerializeField] bool isMainMenu = false;

    private void Start()
    {
        if (isMainMenu)
        {
            SpawnForMenu();
        }
        else
        {
            InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
        }
    }

    void Spawn()
    {

        Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;
        Vector3 spawnPoint = transform.position + spawnDirection;

        float variance = Random.Range(-trayectoryVariance, trayectoryVariance);
        Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

        GameObject spawnedObject = Instantiate(prefab, spawnPoint, rotation);
        spawnedObject.GetComponent<Asteroid>().SetTrayectory(rotation * -spawnDirection);

    }

    void SpawnForMenu()
    {
        for (int i = 0; i < 7; i++)
        {
            Spawn();
        }
    }
}
