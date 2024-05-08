using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{

    public AudioSource buttonClick;
    public AudioSource gameOverSound;
    private GameManager gameManager;

    public void Start() {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        gameOverSound.Play();
    }
     public void RestartGame()
    {
        buttonClick.Play();
        SceneManager.LoadScene("GameScene");
        gameManager.StartGame();
        Debug.Log(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        buttonClick.Play();
        Application.Quit();
    }
}
