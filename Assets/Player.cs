using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    private static Player instance;

    private int[] wallCount = new int[10];  // 구조가 많이 이상합니다만...
    private int score;
    private float speed;

    private Player()
    {
        Debug.Log("constructor detected");
    }

    public void Init(float x, float y)
    {
        score = 0;
        speed = 18 / 5.0f;
        transform.position = new Vector3(x, y, transform.position.z);
    }

    public static Player GetInstance()
    {
        if (instance == null)
        {
            instance = (Player)GameObject.FindObjectOfType(typeof(Player));
        }
        return instance;
    }

    public int IncScore(int add)
    {
        score += add;
        Debug.Log(score);
        return score;
    }

    public void WallPassed(int timeQuantum)
    {
        int idx = timeQuantum % 10;
        wallCount[idx]++;
        if (wallCount[idx] == 2)
        {
            Debug.Log("wall passed");
            IncScore(1);
            wallCount[idx] = 0;
        }
    }

    public int GetScore()
    {
        return score;
    }

	// Use this for initialization
	void Start () {
        Init(-6, 0);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
