using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private CanvasGroup UIgroup;

    public bool hasGameEnded = false;
    
    public float restartDelay = 1f;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (gameOverUI.activeInHierarchy)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (hasGameEnded)
        {
            UIgroup.alpha += 0.1f;
        }
    }

    public void EndGame()
    {
        if (!hasGameEnded)
        {
            gameOverUI.SetActive(true);
            Time.timeScale = 0f;
            hasGameEnded = true;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        // WHEN MAIN MENU SCENE IS MADE UNCOMMENT
        SceneManager.LoadScene("startscreen");
    }
}
