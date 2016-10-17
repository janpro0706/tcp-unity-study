using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    private static Player instance;

    private IEnumerator sineEaser;
    public Camera cam;
    private float camWidth, camHeight;
    
    private int score = 0;
    private Rigidbody2D rigid;
    private float speedX;
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

        rigid = gameObject.GetComponent<Rigidbody2D>();

        float minSpeed = (camWidth + 1) / 5.0f;
        float maxSpeed = (camWidth + 1) / 1.0f;

        speedX = minSpeed;
        transform.position = new Vector3(X_POS, Y_POS, transform.position.z);

        sineEaser = Assets.EasingFunction.Sine(minSpeed, maxSpeed, 60 * 1000);
        
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

    IEnumerator Flap()
    {
        while (true)
        {
            if (Input.anyKey)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, 4);
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
