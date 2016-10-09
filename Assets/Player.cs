using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    private static Player instance;
    public Camera cam;
    private float camWidth, camHeight;

    private int[] wallCount = new int[10];  // 구조가 많이 이상합니다만...
    private int score;
    private float speedX;
    private float speedY;

    private Player()
    {

    }

    public static Player GetInstance()
    {
        if (instance == null)
        {
            instance = (Player)GameObject.FindObjectOfType(typeof(Player));
        }
        return instance;
    }

    // Use this for initialization
    void Start()
    {
        Init(-6, 0);

        StartCoroutine(Falling());
        StartCoroutine(Flap());
    }

    // Update is called once per frame
    void Update()
    {
        // Increase Player(Wall) speed
        if ((camWidth + 1) / speedX > 0.5f)
        {
            speedX += 0.01f;
        }
    }

    IEnumerator Falling()
    {
        float G = -3f;
        while (true)
        {
            speedY += (G * Time.deltaTime);
            transform.Translate(Vector3.up * speedY * Time.deltaTime);
            yield return false;
        }
    }

    IEnumerator Flap()
    {
        while (true)
        {
            if (Input.anyKey)
            {
                speedY = 3;
            }
            yield return false;
        }
    }

    public void Init(float x, float y)
    {
        cam = Camera.main;
        camHeight = cam.orthographicSize * 2;
        camWidth = camHeight * cam.aspect;

        score = 0;
        speedX = (camWidth + 1) / 5.0f;
        speedY = 0f;
        transform.position = new Vector3(x, y, transform.position.z);
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

    public float GetSpeedX()
    {
        return speedX;
    }
}
