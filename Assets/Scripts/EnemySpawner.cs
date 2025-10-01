using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{

    public GameObject asteroidPrefabs;                          // No necesario con pooling
    public float spawnRatePerMinute = 30f;
    public float spawnRateIncrement = 1f;
    public float xLimit;
    //public float maxTimeLife = 10f;       // en la Asteroid.cs
    public AsteroidPool asteroidPool;

    private float spawnNext = 0;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > spawnNext)
        {
            spawnNext = Time.time + 60 / spawnRatePerMinute;

            spawnRatePerMinute += spawnRateIncrement;

            float rand = Random.Range(-xLimit, xLimit);

            Vector2 spawnPosition = new Vector2(rand, 8f);

            //GameObject meteor = Instantiate(asteroidPrefabs, spawnPosition, Quaternion.identity);     // No necesario con pooling
            GameObject asteroid = asteroidPool.RequestAsteroid();
            asteroid.transform.position = spawnPosition;
            asteroid.transform.rotation = Quaternion.identity;

            //Destroy(meteor, maxTimeLife);                     // No necesario con pooling; en Asteroid.cs    
        }
    }
}
