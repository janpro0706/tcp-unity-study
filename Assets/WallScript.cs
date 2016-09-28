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
        fadeOutIn = FadeOutIn(5000);
    }

    // Update is called once per frame
    void Update () {
        if (fadeOutIn.MoveNext() != false)
        {
            MoveLeft();
        }
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
        transform.position = new Vector3(pos.x - ((camWidth + 1) / 5.0f * Time.deltaTime), pos.y, pos.z);   // speed is 18 / 5 (벽이 5초 동안 카메라를 완전히 벗어나는 속도)
    }
    
    private IEnumerator FadeOutIn(long millis)
    {
        float countdown = millis / 1000.0f;

        while ((countdown -= Time.deltaTime) > 0) yield return false;

        // fade out wall object
        //    gameObject.SetActive(false); 이부분 아마 프리팹을 비활성화 시켜서 5초 뒤 Instantiate가 안 먹는듯
        GetComponent<Renderer>().enabled = false;   // 사용되지 않는 오브젝트를 지우지 않으므로 나중에 바꾸자

        // increase player score
        Player p = Player.GetInstance();
        p.WallPassed(timeQuantum);

        fadeOut = true;

        yield return true;
    }
}