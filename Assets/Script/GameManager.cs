using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
    private static GameManager instance;

    private WallSpawner wallSpawner;
    public GameObject gameOverView;
    public Button restartBtn;
    private ScoreView scoreText;

    private Player player;
    private int score;

    public static GameManager GetInstance()
    {
        if (instance == null)
        {
            instance = (GameManager)GameObject.FindObjectOfType(typeof(GameManager));
        }
        return instance;
    }

	void Awake()
    {
        player = Player.GetInstance();
        scoreText = ScoreView.GetInstance();
        wallSpawner = WallSpawner.GetInstance();

        restartBtn.onClick.AddListener(Restart);

        Invoke("InitGame", 0.0f);
    }

    void InitGame()
    {
        wallSpawner.gameObject.SetActive(true);
        wallSpawner.Init();
        //wallSpawner.ResetWalls();
        player.Init();

        score = 0;
        scoreText.SetScore(score);

        gameOverView.SetActive(false);
    }

    public void GameOver()
    {
        wallSpawner.gameObject.SetActive(false);
        gameOverView.SetActive(true);

        var rankManager = Assets.RankFileManager.GetInstance();
        int maxScore = rankManager.GetMaxScore();

        var textUIs = gameOverView.GetComponentsInChildren<Text>();

        textUIs[0].text = "Max Score: " + maxScore;
        textUIs[1].text = "Final Score: " + score;

        if (score > maxScore)
        {
            rankManager.SetScore(score);
        }
    }

    public void Restart()
    {
        gameOverView.SetActive(false);

        InitGame();
    }

    public int IncScore(int add)
    {
        score += add;
        Debug.Log(score);
        scoreText.SetScore(score);

        return score;
    }

    public int GetScore()
    {
        return score;
    }
}
