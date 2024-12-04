using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // ��??�O�A�n�`����?��Enemy����
    private float spawnIntervalMin = 5f; // �̤p?�j??
    private float spawnIntervalMax = 10f; // �̤j?�j??
    private int spawnedCount = 0;
    private float lastSpawnTime = 0f;
    private float nextSpawnTime = 10f;

    void Update()
    {
        if (spawnedCount < 5 && Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime += Random.Range(spawnIntervalMin, spawnIntervalMax);
            lastSpawnTime = Time.time;
            spawnedCount++;
        }
    }

    void SpawnEnemy()
    {
        Vector3 randomPosition = new Vector3(
            transform.position.x + Random.Range(-2f, 2f),
            transform.position.y,
            transform.position.z + Random.Range(-2f, 2f)
        );
        GameObject enemyInstance = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
    }
}