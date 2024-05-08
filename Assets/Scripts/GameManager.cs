using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // public GameObject foodPrefab;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI gameOverText;
    public AudioSource gamePlay;
    public AudioSource buttonClick;
    public bool gameIsActive = false;
    public float score = 0;
    public float highScore = 0;
    public Button restartButton;
    public Button quitButton;
    public Button pauseButton;
    public Button resumeButton;
    public Button mainMenuButton;
    public GameObject controllerButton;
    public GameObject wall;
    private Snake snake;
    // public GameObject titleScreen;

    private bool intToBool(int intValue)
    {
        return intValue == 1 ? true : false;
    }

    void Start()
    {
        snake = FindObjectOfType<Snake>();
        highScore = PlayerPrefs.GetFloat("HighScore", 0);
        bool isMode = intToBool(PlayerPrefs.GetInt("isMode"));
        bool isLevel = intToBool(PlayerPrefs.GetInt("isLevel"));
        if(isMode == true){
            controllerButton.SetActive(true);
        }
        else{
            controllerButton.SetActive(false);
        }
        if(isLevel == true){
            wall.SetActive(true);
        }
        else{
            wall.SetActive(false);
        }
        StartGame();
    }

    void Update()
    {

    }

    // public void SpawnTarget()
    // {
    //     if (gameIsActive)
    //     {
    //         float x = Random.Range(-8, 8);
    //         float y = Random.Range(-4, 4);
    //         Instantiate(foodPrefab, new Vector3(x, y, 0), Quaternion.identity);
    //         scoreText.text = "Score:" + score;

    //         UpdateHighScore();
    //     }
    // }

    public void UpdateScore(int scoreToAdd)
    {
        if(score > 5){
            print(score);
            snake.speed = 1.5f;
        }
        if(score > 15){
            snake.speed = 2;  
        }
        if(score > 50){
            snake.speed = 3;
        }

        if (FindObjectOfType<Snake>().isPowerUp == true)
        {
            score += scoreToAdd * 2;
        }
        else
        {
            score += scoreToAdd;
        }

        scoreText.text = "Score: " + score;
    }

    public void UpdateHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", highScore);
            UpdateHighScoreText();
        }
    }

    public void UpdateHighScoreText()
    {
        highScoreText.text = "High Score: " + highScore;
    }

    public void GameOver()
    {
        // FindObjectOfType<SnakeMovement>().rb.velocity = Vector2.zero;
        gameIsActive = false;
        SceneManager.LoadScene("GameOverScene");
        // restartButton.gameObject.SetActive(true);
        // quitButton.gameObject.SetActive(true);
        // gameOverText.gameObject.SetActive(true);
        // pauseButton.gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        buttonClick.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        buttonClick.Play();
        Application.Quit();
    }

    public void StartGame()
    {
        gamePlay.Play();
        // titleScreen.gameObject.SetActive(false);
        gameIsActive = true;
        // SpawnTarget();
        UpdateScore(0);
        UpdateHighScoreText();
    }

    public void PauseGame()
    {
        buttonClick.Play();
        Time.timeScale = 0;
        resumeButton.gameObject.SetActive(true);
        mainMenuButton.gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        buttonClick.Play();
        Time.timeScale = 1;
        resumeButton.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
    }

    public void MainMenuButton()
    {
        buttonClick.Play();
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuScene");
    }
}