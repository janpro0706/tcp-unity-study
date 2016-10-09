using UnityEngine;
using System.Collections;

public class WallTranslator : MonoBehaviour {
    private IEnumerator fadeOutIn;
    public Camera cam;
    private float camWidth, camHeight;
    private Player player;

    // Use this for initialization
    void Start () {
        cam = Camera.main;
        camHeight = cam.orthographicSize * 2;
        camWidth = camHeight * cam.aspect;

        player = Player.GetInstance();
        
        StartCoroutine(FadeOutIn((long)((camWidth + 1) / player.GetSpeedX() * 1000)));
    }

    private void MoveLeft()
    {
        var pos = transform.position;
        transform.Translate(Vector3.left * (player.GetSpeedX() * Time.deltaTime));       // speed is 18 / 5 (벽이 5초 동안 카메라를 완전히 벗어나는 속도)
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