using UnityEngine;
using System.Collections;

public class GenWall : MonoBehaviour {
    public Transform wall;
    public Camera cam;

    float camWidth, camHeight;

    // Use this for initialization
    void Start () {
        cam = Camera.main;
        camHeight = cam.orthographicSize * 2;
        camWidth = camHeight * cam.aspect;

        //float[] p = RandomGenPosition();
        //GenerateWall(p[0], p[1]);
        GenerateWall(Random.Range(-10.0f, 10.0f));
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
                GenerateWall(Random.Range(-10.0f, 10.0f));

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

    void GenerateWall(float y)
    {
        Instantiate(wall, new Vector3(camWidth / 2, y, 0), Quaternion.identity);
    }

    float[] RandomGenPosition()
    {
        float b = 0, t = 0;

        while (Mathf.Abs(t - b) < 3)
        {
            b = Random.Range(-5.0f, 5.0f);
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
}
