using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{

    public float speed = 20f;
    public float maxLifeTime = 3f;
    public Vector3 targetVector;
    public GameObject asteroidPrefabs;                              // No necesario con pooling
    public AsteroidPool asteroidPool;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Destroy(gameObject, maxLifeTime);                         // No necesario con pooling
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * targetVector * Time.deltaTime, Space.World);
    }

    void OnEnable()
    {
        // Asignar el pool al no poder arrastrar scripts en campos de componentes de prefabs
        if (asteroidPool == null)
        {
            asteroidPool = GameObject.FindGameObjectWithTag("AsteroidPool").GetComponent<AsteroidPool>();
        }

        Invoke(nameof(DeactivateBullet), maxLifeTime);              // Desactivar la bala si ha llegado al tiempo máximo de vida
    }

    void OnDisable()
    {
        CancelInvoke();                                         
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IncreaseScore();
            GameObject asteroid = collision.gameObject;
            float asteroidSize = asteroid.transform.localScale.x;   // Tamaño actual del asteroide

            if (asteroidSize > 0.2f)
            {
                Vector3 bulletDirection = targetVector.normalized;  // Vector dirección de la bala usado para calcular los nuevos vectores de los asteroides

                Vector3 direction1 = Quaternion.AngleAxis(45f, Vector3.forward) * bulletDirection;      // (forward) Respecto al eje Z, rotando en el plano XY
                Vector3 direction2 = Quaternion.AngleAxis(-45f, Vector3.forward) * bulletDirection;

                //GameObject asteroid2 = Instantiate(asteroidPrefabs, asteroid.transform.position, Quaternion.identity);        // No necesario con pooling
                GameObject asteroid2 = asteroidPool.RequestAsteroid();
                asteroid2.transform.position = asteroid.transform.position;
                asteroid2.transform.rotation = Quaternion.identity;

                asteroid.transform.localScale *= 0.5f;              // Minimizar el tamaño de los asteroides a la mitad
                asteroid2.transform.localScale *= 0.5f;

                float force = 5f;

                asteroid.GetComponent<Rigidbody>().AddForce(direction1 * force);        // Añadir un impulso en la nueva dirección
                asteroid2.GetComponent<Rigidbody>().AddForce(direction2 * force);
            }
            else
            {
                //Destroy(asteroid);                                // No necesario con pooling
                asteroid.SetActive(false);
            }
            //Destroy(gameObject);                                  // No necesario con pooling
            DeactivateBullet();
        }
    }

    private void IncreaseScore()
    {
        Player.SCORE++;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        GameObject go = GameObject.FindGameObjectWithTag("UI");
        go.GetComponent<Text>().text = "Puntos: " + Player.SCORE;
    }

    private void DeactivateBullet()
    {
        gameObject.SetActive(false);
    }
}


