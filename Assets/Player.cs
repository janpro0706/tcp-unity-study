using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    private static Player instance;

    private IEnumerator sineEaser;
    public Camera cam;
    private float camWidth, camHeight;
    
    private int score = 0;
    private float speedX;
    private float speedY;
    public const float X_POS = -6;
    public const float Y_POS = 0;

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
        cam = Camera.main;
        camHeight = cam.orthographicSize * 2;
        camWidth = camHeight * cam.aspect;

        float minSpeed = (camWidth + 1) / 5.0f;
        float maxSpeed = (camWidth + 1) / 1.0f;

        speedX = minSpeed;
        speedY = 0;
        transform.position = new Vector3(X_POS, Y_POS, transform.position.z);

        sineEaser = Assets.EasingFunction.Sine(minSpeed, maxSpeed, 60 * 1000);

        StartCoroutine(Falling());
        StartCoroutine(Flap());
    }

    // Update is called once per frame
    void Update()
    {
        // Increase Player(Wall) speed easing in Sine
        if (sineEaser.MoveNext() != false)
        {
            speedX = (float)sineEaser.Current;
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
