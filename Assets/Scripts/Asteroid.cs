using UnityEngine;

public class Asteroid : MonoBehaviour
{

    public float maxTimeLife = 10f;

    void OnEnable()
    {
        Invoke(nameof(DeactivateAsteroid), maxTimeLife);
    }

    void OnDisable()
    {
        CancelInvoke();                                         // Se cancela si el objeto se desactiva
    }

    private void DeactivateAsteroid()
    {
        gameObject.SetActive(false);
    }

    public void ResetAsteroid()
    {
        gameObject.transform.localScale = new Vector3(0.4f, 0.4f, 1f);
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.linearVelocity = new Vector3(0f, 0f, 0f); 
        rb.angularVelocity = new Vector3(0f, 0f, 0f);
    }
}
