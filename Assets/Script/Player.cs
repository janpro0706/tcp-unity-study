using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    private static Player instance;
    ScreenManager screenManager;
    private IEnumerator sineEaser;
    
    private Rigidbody2D rigid;
    private float speedX;
    public const float X_POS = -6;
    public const float Y_POS = 0;
    public bool isAlive;


    public static Player GetInstance()
    {
        if (instance == null)
        {
            instance = (Player)GameObject.FindObjectOfType(typeof(Player));
        }
        return instance;
    }

    void Awake()
    {
        screenManager = ScreenManager.GetInstance();
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start()
    {
        Init();
    }

    void Init()
    {
        StopCoroutine("Flap");
        StopCoroutine("SpeedUp");
        
        isAlive = true;
        
        transform.position = new Vector3(X_POS, Y_POS, transform.position.z);
        rigid.velocity = new Vector2(rigid.velocity.x, 0);

        StartCoroutine("Flap");
        StartCoroutine("SpeedUp");
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

    IEnumerator SpeedUp()
    {
        float camWidth = screenManager.GetCamWidth();

        float minSpeed = (camWidth + 1) / 5.0f;
        float maxSpeed = (camWidth + 1) / 1.0f;

        speedX = minSpeed;

        sineEaser = Assets.EasingFunction.Sine(minSpeed, maxSpeed, 60 * 1000);
        // Increase Player(Wall) speed easing in Sine
        while (sineEaser.MoveNext())
        {
            speedX = (float)sineEaser.Current;
            yield return true;
        }
    }

    public float GetSpeedX()
    {
        return speedX;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("bird collided");

        isAlive = false;
    }

    public bool IsAlive()
    {
        return isAlive;
    }
}
