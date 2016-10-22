using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    private static Player instance;

    private IEnumerator sineEaser;
    public Camera cam;
    private float camWidth, camHeight;
    
    private Rigidbody2D rigid;
    private float speedX;
    public const float X_POS = -6;
    public const float Y_POS = 0;
    public bool isAlive;

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

        Init();
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

    void Init()
    {
        StopCoroutine("Flap");

        float minSpeed = (camWidth + 1) / 5.0f;
        float maxSpeed = (camWidth + 1) / 1.0f;

        isAlive = true;

        speedX = minSpeed;
        transform.position = new Vector3(X_POS, Y_POS, transform.position.z);
        rigid.velocity = new Vector2(rigid.velocity.x, 0);

        sineEaser = Assets.EasingFunction.Sine(minSpeed, maxSpeed, 60 * 1000);

        StartCoroutine(Flap());
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
