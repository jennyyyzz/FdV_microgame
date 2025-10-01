using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public float thrustForce = 100f;            // fuerza de empuje
    public float rotationSpeed = 120f;          // velocidad de rotación
    public GameObject gun;
    public GameObject bulletPrefab;             // No necesario con pooling
    public BulletPool bulletPool;

    private Rigidbody _rigid;

    public static int SCORE = 0;
    public static float xBorderLimit, yBorderLimit;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()                                // Sólo se ejecuta una vez, en el primer frame
    {
        _rigid = GetComponent<Rigidbody>();     // Se inicializa

        yBorderLimit = Camera.main.orthographicSize + 1;
        xBorderLimit = (Camera.main.orthographicSize + 1) * Screen.width / Screen.height;
    }

    // Update is called once per frame
    void Update()   // se ejecuta siempre al final de cada frame
    {
        float rotation = Input.GetAxis("Rotate") * Time.deltaTime;
        float thrust = Input.GetAxis("Thrust") * Time.deltaTime;    // (Time.deltaTime) Para ajustar a la velocidad de frames

        Vector3 thrustDirection = transform.right;

        _rigid.AddForce(thrustDirection * thrust * thrustForce);

        transform.Rotate(Vector3.forward, -rotation * rotationSpeed);    // Vector sobre el que aplica el giro, ángulo de giro

        var newPos = transform.position;
        if (newPos.x > xBorderLimit)
            newPos.x = -xBorderLimit + 1;
        else if (newPos.x < -xBorderLimit)
            newPos.x = xBorderLimit - 1;
        else if (newPos.y > yBorderLimit)
            newPos.y = -yBorderLimit + 1;
        else if (newPos.y < -yBorderLimit)
            newPos.y = yBorderLimit - 1;
        transform.position = newPos;

        if (!Pause.paused && Input.GetKeyDown(KeyCode.Space))
        {
            //GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);   // No necesario con pooling
            GameObject bullet = bulletPool.RequestBullet();
            bullet.transform.position = gun.transform.position;
            bullet.transform.rotation = Quaternion.identity;

            Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponent<Collider>());             // Para evitar que la nave se eche para atrás al lanzar balas

            Bullet balaScript = bullet.GetComponent<Bullet>();
            balaScript.targetVector = transform.right;
        }


    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SCORE = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            Debug.Log("He colisionado con otra cosa...");
        }
    }
}
