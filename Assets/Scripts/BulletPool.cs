using UnityEngine;
using System.Collections.Generic;


public class BulletPool : MonoBehaviour
{

    public GameObject bulletPrefab;
    public int poolSize = 10;
    public List<GameObject> bulletList = new List<GameObject>();            // Lista con las balas del juego

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AddBulletToPool(poolSize);
    }

    private void AddBulletToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletList.Add(bullet);
            bullet.transform.parent = transform;
        }
    }

    public GameObject RequestBullet()
    {
        for (int i = 0; i < bulletList.Count; i++)
        {
            if (!bulletList[i].activeSelf)
            {
                bulletList[i].SetActive(true);
                return bulletList[i];
            }
        }
        AddBulletToPool(1);
        bulletList[bulletList.Count - 1].SetActive(true);
        return bulletList[bulletList.Count - 1];
    }
}
