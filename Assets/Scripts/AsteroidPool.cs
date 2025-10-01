using UnityEngine;
using System.Collections.Generic;

public class AsteroidPool : MonoBehaviour
{

    public GameObject asteroidPrefab;
    public int poolSize = 5;
    public List<GameObject> asteroidList = new List<GameObject>();          // Lista con los asteroides del juego

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AddAsteroidToPool(poolSize);
    }

    private void AddAsteroidToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject asteroid = Instantiate(asteroidPrefab);
            asteroid.SetActive(false);
            asteroid.transform.parent = transform;                          // Para tener la jerarquía ordenada

            asteroidList.Add(asteroid);
        }
    }

    public GameObject RequestAsteroid()
    {
        Asteroid asteroidScript;
        for (int i = 0; i < asteroidList.Count; i++)
        {
            if (!asteroidList[i].activeSelf)
            {
                asteroidScript = asteroidList[i].GetComponent<Asteroid>();
                asteroidScript.ResetAsteroid();                                         // Para que vuelvan al tamaño inicial si se reduce su tamaño
                asteroidList[i].SetActive(true);
                return asteroidList[i];
            }
        }
        AddAsteroidToPool(1);                                               // En caso de que el pool no sea lo suficientemente grande, ampliarlo
        asteroidScript = asteroidList[asteroidList.Count - 1].GetComponent<Asteroid>();
        asteroidScript.ResetAsteroid();
        //ResetAsteroid(asteroidList[asteroidList.Count - 1]);
        asteroidList[asteroidList.Count - 1].SetActive(true);
        return asteroidList[asteroidList.Count - 1];
    }
}
