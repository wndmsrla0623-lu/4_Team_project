
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;
    public float spawnInterval = 2f;
    public int maxAlive = 10;

    float timer;
    bool canSpawn = true;

    public void SetSpawning(bool enabled)
    {
        canSpawn = enabled;

        if (enabled)
            timer = 0f;
    }

    void Update()
    {
        if (!canSpawn) return;

        timer += Time.deltaTime;

        if (timer < spawnInterval) return;

        timer = 0f;

        int alive =
            GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (alive >= maxAlive) return;

        if (enemyPrefabs.Length == 0 ||
            spawnPoints.Length == 0) return;

        int e = Random.Range(0, enemyPrefabs.Length);
        int s = Random.Range(0, spawnPoints.Length);

        Vector3 pos = spawnPoints[s].position;

        Instantiate(enemyPrefabs[e], pos, Quaternion.identity);
    }
}
