using UnityEngine;
using System.Collections;

public class GenWall : MonoBehaviour {
    public Transform wall;
    public Camera cam;

    private float camWidth, camHeight;
    private int timeQuantum;

    // Use this for initialization
    void Start () {
        cam = Camera.main;
        camHeight = cam.orthographicSize * 2;
        camWidth = camHeight * cam.aspect;

        timeQuantum = 1;
        //float[] p = RandomGenPosition();
        //GenerateWall(p[0], p[1]);
        float[] pos = RandomGenYPositions(3);
        GenerateWall(pos[0], timeQuantum);
        GenerateWall(pos[1], timeQuantum);
        timeQuantum++;

        StartCoroutine(GenerateInterval(5));
	}
	
	// Update is called once per frame
	void Update () {
	}

    IEnumerator GenerateInterval(int sec)
    {
        float count = sec;

        while (true)
        {
            count -= Time.deltaTime;
            if (count > 0) yield return false;
            else
            {
                // Generate Wall from prefab
                //float[] p = RandomGenPosition();
                //GenerateWall(p[0], p[1]);
                float[] pos = RandomGenYPositions(3);
                GenerateWall(pos[0], timeQuantum);
                GenerateWall(pos[1], timeQuantum);
                timeQuantum++;

                count += sec;
                yield return true;
            }
        }
    }

    // scale 문제 때문에 제대로 동작하지 않음
    void GenerateWall(float bottom, float top)
    {
        float height = top - bottom;
        float y = bottom + height / 2.0f;
        float scaleY = height / 10.0f / 2.0f;

        Object instance = Instantiate(wall, new Vector3(camWidth / 2, y, 0), Quaternion.identity);
    }

    void GenerateWall(float y, int timeQuantum)
    {
        Instantiate(wall, new Vector3(camWidth / 2, y, 0), Quaternion.identity);
    }

    // 하나의 벽의 top bottom을 받아와 스케일 하려고 했는데.. 잘 안됬음
    // b: bottom of wall, t: top of wall
    float[] RandomGenPosition()
    {
        float b = 0, t = 0;

        while (Mathf.Abs(t - b) < 3)
        {
            b = Random.Range(-5.0f, 5.0f);  // (-5.0f, 5.0f) coord of bottom and top of camera
            t = Random.Range(-5.0f, 5.0f);
        }

        if (b > t)
        {
            float temp = b;
            b = t;
            t = temp;
        }

        float[] pos = { b, t };

        return pos;
    }

    // get random y coord of two walls with space
    // b: bottom wall, t: top wall
    float[] RandomGenYPositions(float space)
    {
        float b = 0, t = 0;

        while (Mathf.Abs(t - b) < 10 + space)
        {
            b = Random.Range(-10.0f, 10.0f);    // camera height + wall padding
            t = Random.Range(-10.0f, 10.0f);
        }

        if (b > t)
        {
            float temp = b;
            b = t;
            t = temp;
        }

        float[] pos = { b, t };

        return pos;
    }
}
