using UnityEngine;
using System.Collections;

public class GenWall : MonoBehaviour {
    public Transform wallPrefab;
    public GameObject passDetectorPrefab;
    public Camera cam;
    private float camWidth, camHeight;

    private Queue wallQueue = new Queue();
    private const int WALL_NUM = 3;

    void Start () {
        cam = Camera.main;
        camHeight = cam.orthographicSize * 2;
        camWidth = camHeight * cam.aspect;
        
        StartCoroutine(GenerateInterval(2));
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

                WallFamily fam;
                if (wallQueue.Count < WALL_NUM)
                {
                    fam = new WallFamily();
                    fam.upWall = GenerateWall(pos[0]);
                    fam.downWall = GenerateWall(pos[1]);
                    fam.passDetector = GenerateDetector();

                    wallQueue.Enqueue(fam);
                }

                count += sec;
                yield return true;
            }
        }
    }

    Object GenerateWall(float y)
    {
        Object wall = Instantiate(wallPrefab, new Vector3(camWidth / 2, y, 0), Quaternion.identity);
        return wall;
    }

    Object GenerateDetector()
    {
        Object passDetector = Instantiate(passDetectorPrefab, new Vector3(camWidth / 2, 0, 0), Quaternion.identity);
        return passDetector;
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
