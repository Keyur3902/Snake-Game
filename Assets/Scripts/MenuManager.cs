using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public AudioSource startGame;
    public AudioSource buttonClick;
    public GameObject titleScreen;
    public GameObject settingScreen;
    public GameObject controlScreen;
    public GameObject audioScreen;
    private GameManager gameManager;
    public GameObject levelScreen;
    
    void Start()
    {
        startGame.Play();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void StartButton()
    {
        buttonClick.Play();
        SceneManager.LoadScene("GameScene");
        gameManager.StartGame();
    }

    public void SettingButton()
    {
        buttonClick.Play();
        titleScreen.gameObject.SetActive(false);
        settingScreen.gameObject.SetActive(true);
        controlScreen.gameObject.SetActive(true);
    }

    public void QuitButton()
    {
        buttonClick.Play();
        Application.Quit();
    }

    public void ControlsButton()
    {
        buttonClick.Play();
        audioScreen.gameObject.SetActive(false);
        controlScreen.gameObject.SetActive(true);
        levelScreen.gameObject.SetActive(false);
    }

    public void AudioButton()
    {
        buttonClick.Play();
        audioScreen.gameObject.SetActive(true);
        controlScreen.gameObject.SetActive(false);
        levelScreen.gameObject.SetActive(false);
    }

    public void ReturnButton()
    {
        buttonClick.Play();
        controlScreen.gameObject.SetActive(false);
        audioScreen.gameObject.SetActive(false);
        settingScreen.gameObject.SetActive(false);
        titleScreen.gameObject.SetActive(true);
        levelScreen.gameObject.SetActive(false);
    }

    public void MuteButton()
    {
        AudioListener.volume = 0;
    }

    public void UnMuteButton()
    {
        buttonClick.Play(); 
        AudioListener.volume = 1;
    }

    private int boolToInt(bool booleanValue)
    {
        return booleanValue ? 1 : 0;
    }

    public void ControllerButton()
    {
        buttonClick.Play();
        PlayerPrefs.SetInt("isMode", boolToInt(true));
    }

    public void ControllerSwipe()
    {
        buttonClick.Play();
        PlayerPrefs.SetInt("isMode", boolToInt(false));
    }

    public void KeyboardController(){
        buttonClick.Play();
        PlayerPrefs.SetInt("isMode", boolToInt(false));
    }

    public void SelectWallLevel(){
        buttonClick.Play();
        PlayerPrefs.SetInt("isLevel", boolToInt(true));
    }

    public void SelectSimpleLevel(){
        buttonClick.Play();
        PlayerPrefs.SetInt("isLevel", boolToInt(false));
    }

    public void LevelsButton(){
        buttonClick.Play();
        audioScreen.gameObject.SetActive(false);
        controlScreen.gameObject.SetActive(false);
        levelScreen.gameObject.SetActive(true);
    }
}
