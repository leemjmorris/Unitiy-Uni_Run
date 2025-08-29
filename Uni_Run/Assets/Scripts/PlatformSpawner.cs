using UnityEngine;
using System.Collections.Generic;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public int poolSize = 10;
    public float spawnX = 12f;

    public float minY = -2f;
    public float maxY = 2f;
    public float spawnInterval = 2f;

    public float despawnX = -12f;

    private Queue<GameObject> platformPool;
    private float nextSpawnTime;

    private void Awake()
    {
        InitializePool();

        SpawnPlatform();
        nextSpawnTime = Time.time + spawnInterval;

    }

    private void InitializePool()
    {
        platformPool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject platform = Instantiate(platformPrefab, transform);
            platform.SetActive(false);
            platformPool.Enqueue(platform);
        }
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameOver) return;

        foreach(Transform child in transform)
        {
            if (child.gameObject.activeSelf && child.position.x < despawnX)
            {
                RecyclePlatform(child.gameObject);
            }
        }

        if (Time.time >= nextSpawnTime)
        {
            SpawnPlatform();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void SpawnPlatform()
    {
        if (platformPool.Count == 0) return;

        GameObject platform = platformPool.Dequeue();

        float yPos = Random.Range(minY, maxY);
        platform.transform.position = new Vector3(spawnX, yPos, 0);
        platform.SetActive(true);
    }

    private void RecyclePlatform(GameObject platform)
    {
        platform.SetActive(false);
        platformPool.Enqueue(platform);
    }
}
