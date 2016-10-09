using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour {
    private IEnumerator fadeOutIn;
    public Camera cam;

    private int timeQuantum;                // 시간 순서대로 생성된 벽을 1부터 매김. 동시간에 생성된 벽은 같은 값
    private float camWidth, camHeight;
    private bool fadeOut;

    // Use this for initialization
    void Start () {
        cam = Camera.main;
        camHeight = cam.orthographicSize * 2;
        camWidth = camHeight * cam.aspect;

        fadeOut = false;
        StartCoroutine(FadeOutIn(5000));
    }

    // Update is called once per frame
    void Update () {
    }


    public void SetTimeQuantum(int timeQuantum)
    {
        this.timeQuantum = timeQuantum;
    }

    public int GetTimeQuantum()
    {
        return timeQuantum;
    }

    private void MoveLeft()
    {
        var pos = transform.position;
        //transform.position = new Vector3(pos.x - ((camWidth + 1) / 5.0f * Time.deltaTime), pos.y, pos.z);   // speed is 18 / 5 (벽이 5초 동안 카메라를 완전히 벗어나는 속도)
        transform.Translate(Vector3.left * ((camWidth + 1) / 5.0f * Time.deltaTime));
    }
    
    private IEnumerator FadeOutIn(long millis)
    {
        float countdown = millis / 1000.0f;

        while ((countdown -= Time.deltaTime) > 0)
        {
            MoveLeft();

            yield return false;
        }

        // increase player score
        Player p = Player.GetInstance();
        p.WallPassed(timeQuantum);

        fadeOut = true;

        // fade out wall object
        Destroy(gameObject);

        yield return true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collision detected: " + other.name);
    }
}