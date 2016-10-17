using UnityEngine;
using System.Collections;

public class WallSpawner : MonoBehaviour {
    public GameObject wallPrefab;
    public GameObject passDetectorPrefab;
    public Camera cam;
    private float camWidth, camHeight;

    private Queue wallQueue = new Queue();
    private const int WALL_NUM = 3;

    void Start () {
        cam = Camera.main;
        camHeight = cam.orthographicSize * 2;
        camWidth = camHeight * cam.aspect;


        InstantiateWalls();
        StartCoroutine(SpawnInterval(2));
	}

    void InstantiateWalls()
    {
        for (int i = 0; i < WALL_NUM; i++)
        {
            float[] pos = RandomGenYPositions(3);

            WallFamily fam = new WallFamily();
            fam.upWall = SpawnWall(pos[0]);
            fam.downWall = SpawnWall(pos[1]);
            fam.passDetector = SpawnDetector();

            wallQueue.Enqueue(fam);
        }
    }

    IEnumerator SpawnInterval(int sec)
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

                WallFamily fam = (WallFamily)wallQueue.Dequeue();
                fam.upWall.SetActive(true);
                fam.downWall.SetActive(true);
                fam.passDetector.SetActive(true);

                wallQueue.Enqueue(fam);

                count += sec;
                yield return true;
            }
        }
    }

    GameObject SpawnWall(float y)
    {
        GameObject wall = (GameObject)Instantiate(wallPrefab, new Vector3(camWidth / 2, y, 0), Quaternion.identity);
        wall.SetActive(false);
        return wall;
    }

    GameObject SpawnDetector()
    {
        GameObject passDetector = (GameObject)Instantiate(passDetectorPrefab, new Vector3(camWidth / 2, 0, 0), Quaternion.identity);
        passDetector.SetActive(false);
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
