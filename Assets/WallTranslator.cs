using UnityEngine;
using System.Collections;

public class WallTranslator : MonoBehaviour {
    private IEnumerator fadeOutIn;
    public Camera cam;
    private float camWidth, camHeight;

    // Use this for initialization
    void Start () {
        cam = Camera.main;
        camHeight = cam.orthographicSize * 2;
        camWidth = camHeight * cam.aspect;
        
        StartCoroutine(FadeOutIn(5000));
    }

    // Update is called once per frame
    void Update () {
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

        // fade out wall object
        Destroy(gameObject);

        yield return true;
    }
}