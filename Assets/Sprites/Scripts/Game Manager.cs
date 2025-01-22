using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Intro,
    Playing,
    Dead,
}


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState State = GameState.Intro;
    public float PlayStartTime;
    public int Lives = 3;

    [Header("References")]
    public GameObject IntroUI;
    public GameObject DeadUI;
    public GameObject EnemySpawner;
    public GameObject FoodSpawner;
    public GameObject GoldenSpawner;

    public Player PlayerScript;
    public TMP_Text scoreText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IntroUI.SetActive(true);

    }

    float CalculateScore()
    {
        return Time.time - PlayStartTime;
    }

    void SaveHighScore()
    {
        float score = Mathf.FloorToInt(CalculateScore());
        int currnetHighScore = PlayerPrefs.GetInt("highScore");
        if (score > currnetHighScore)
        {
            PlayerPrefs.SetInt("highScore", (int)score);
            PlayerPrefs.Save();
        }
    }

    int GetHighScore()
    {
        return PlayerPrefs.GetInt("highScore");
    }

    public float CalculateGameSpeed()
    {
        if (State != GameState.Playing)
        {
            return 5f; // initial speed
        }
        float speed = 8f + (0.5f * Mathf.FloorToInt(CalculateScore() / 10f));
        return Mathf.Min(speed, 30f);
    }

    // Update is called once per frame
    void Update()
    {
        if (State == GameState.Playing)
        {
            scoreText.text = "Score: " + Mathf.FloorToInt(CalculateScore()); ;
        }
        else if (State == GameState.Dead)
        {
            scoreText.text = "High Score: " + GetHighScore();
        }


        if (State == GameState.Intro && Input.GetKeyDown(KeyCode.Space))
        {
            State = GameState.Playing;
            IntroUI.SetActive(false);
            EnemySpawner.SetActive(true);
            FoodSpawner.SetActive(true);
            GoldenSpawner.SetActive(true);
            PlayStartTime = Time.time;
        }
        if (State == GameState.Playing && Lives == 0)
        {
            PlayerScript.KillPlayer();
            EnemySpawner.SetActive(false);
            FoodSpawner.SetActive(false);
            GoldenSpawner.SetActive(false);
            State = GameState.Dead;
            DeadUI.SetActive(true);
            SaveHighScore();
        }
        if (State == GameState.Dead && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
