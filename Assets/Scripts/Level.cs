using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{

    [SerializeField] int gameOverDelay = 3;
    GameSession gameSession;

    public void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
        gameSession.ResetScore();
    }

    public void LoadGameOver()
    {
        StartCoroutine(LoadDelay());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadDelay()
    {
        yield return new WaitForSeconds(gameOverDelay);
        SceneManager.LoadScene("GameOver");
    }
}
