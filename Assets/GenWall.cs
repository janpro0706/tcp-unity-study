using UnityEngine;
using System.Collections;

public class GenWall : MonoBehaviour {
    public Transform wall;
    public Camera cam;

    private float camWidth, camHeight;
    private int timeQuantum;
    

    void Start () {
        cam = Camera.main;
        camHeight = cam.orthographicSize * 2;
        camWidth = camHeight * cam.aspect;

        timeQuantum = 1;

        StartCoroutine(GenerateInterval(5));
	}
	
	void Update () {
	}

    IEnumerator GenerateInterval(int sec)
    {
        float count = 0;

        while (true)
        {
            count -= Time.deltaTime;
            if (count > 0) yield return false;
            else
            {
                // Generate Wall from prefab
                float[] pos = RandomGenYPositions(3);
                GenerateWall(pos[0], timeQuantum);
                GenerateWall(pos[1], timeQuantum);
                timeQuantum++;

                count += sec;
                yield return true;
            }
        }
    }

    void GenerateWall(float y, int timeQuantum)
    {
        Object w = Instantiate(wall, new Vector3(camWidth / 2, y, 0), Quaternion.identity);
        
        ((Transform)w).GetComponent<WallScript>().SetTimeQuantum(timeQuantum);
    }

    // get random y coord of two walls with space
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
