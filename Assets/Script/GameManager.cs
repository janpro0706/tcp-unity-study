using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    private static GameManager instance;

    public Player player;
    private int score = 0;

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
    }

    public int IncScore(int add)
    {
        score += add;
        Debug.Log(score);

        return score;
    }

    public int GetScore()
    {
        return score;
    }
}
