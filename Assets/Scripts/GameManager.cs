using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI highScoreText;
    public int score = 0;
    private int highScore;
    private SoundManager soundManager;
    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            //DontDestroyOnLoad(singleton.gameObject);
        }
        else if (singleton != this)
        {
            Destroy(this);
        }

        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = $"High Score: {highScore}";
    }
    void Start()
    {
        soundManager = GameObject.Find("Sound Manager").GetComponent<SoundManager>();
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.buildIndex == 0)
        {
            soundManager.PlayMenuMusic();
        }
        else
        {
            soundManager.PlayBgMusic();
        }
    }

    public void StartGame()
    {

        SceneManager.LoadScene(1);
        Time.timeScale = 1;
        soundManager = SoundManager.instance;
        soundManager.PlayBgMusic();
        soundManager.StopMenuMusic();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        soundManager.PauseBgMusic();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        soundManager.ResumeBGMusic();

    }

    public void IncreaseScore(int increment)
    {
        score += increment;
        scoreText.text = $"Score: {score}";

        //if score is greater than previous high score
        if (score > highScore)
        {
            highScoreText.text = $"High Score: {score}";
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        soundManager.PlayMenuMusic();
    }

    public void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        if (EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
        }
#endif
    }

    private void OnApplicationQuit()
    {
        singleton = null;
    }
}
