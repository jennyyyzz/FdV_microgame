using UnityEngine;

public class Pause : MonoBehaviour
{

    public GameObject pauseMenu;
    public static bool paused = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenu.SetActive(false);                     // Se inicializa sin estar el juego pausado
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                pauseMenu.SetActive(true);
                paused = true;

                Time.timeScale = 0;
            }
            else
            {
                Resume();                               // Si ya estaba pausado, reanudar el juego
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        paused = false;

        Time.timeScale = 1;
    }
}
