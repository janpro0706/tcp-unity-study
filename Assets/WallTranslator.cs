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
        
        StartCoroutine(TranslateWall());
    }

    private void MoveLeft()
    {
        var pos = transform.position;
        transform.Translate(Vector3.left * (player.GetSpeedX() * Time.deltaTime));       // speed is 18 / 5 (벽이 5초 동안 카메라를 완전히 벗어나는 속도)
    }

    private void Init(float x, float y)
    {
        transform.position = new Vector3(x, y, 0);
    }
    
    private IEnumerator TranslateWall()
    {
        while (transform.position.x > -(camWidth / 2 + 1))
        {
            MoveLeft();
            yield return false;
        }

        Init(camWidth / 2 + 1, transform.position.y);

        yield return true;
    }
}