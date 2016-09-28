using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour {
    private IEnumerator fadeOut;
    public Camera cam;

    float camWidth, camHeight;

    // Use this for initialization
    void Start () {
        cam = Camera.main;
        camHeight = cam.orthographicSize * 2;
        camWidth = camHeight * cam.aspect;

        fadeOut = FadeOutIn(5000);
    }

    // Update is called once per frame
    void Update () {
        if (fadeOut.MoveNext() != false)
        {
            MoveLeft();
        }
    }

    void MoveLeft()
    {
        var pos = transform.position;
        transform.position = new Vector3(pos.x - ((camWidth + 1) / 5.0f * Time.deltaTime), pos.y, pos.z);
    }
    
    IEnumerator FadeOutIn(long millis)
    {
        float countdown = millis / 1000.0f;

        while ((countdown -= Time.deltaTime) > 0) yield return false;

        //    gameObject.SetActive(false); 이부분 아마 프리팹을 비활성화 시켜서 5초 뒤 Instantiate가 안 먹는듯
        GetComponent<Renderer>().enabled = false;   // 사용되지 않는 오브젝트를 지우지 않으므로 나중에 바꾸자

        yield return true;
    }
}