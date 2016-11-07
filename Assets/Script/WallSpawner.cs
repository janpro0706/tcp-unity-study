using UnityEngine;
using System.Collections;

public class WallSpawner : MonoBehaviour {
    public static WallSpawner instance;

    public GameObject wallPrefab;
    public GameObject passDetectorPrefab;
    ScreenManager screenManager;

    private Queue wallQueue = new Queue();
    private const int WALL_NUM = 3;

    public static WallSpawner GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<WallSpawner>();
        }
        return instance;
    }

    void Awake()
    {
        screenManager = ScreenManager.GetInstance();

        InstantiateWalls();
    }

    public void Init()
    {
        ResetWalls();
        StartCoroutine(SpawnInterval(2));
    }

    void InstantiateWalls()
    {
        for (int i = 0; i < WALL_NUM; i++)
        {
            float[] pos = RandomGenYPositions(3);

            GameObject wall = SpawnWall(pos[0], pos[1]);

            wallQueue.Enqueue(wall);
        }
    }

    public IEnumerator SpawnInterval(int sec)
    {
        float count = 0.5f;

        while (true)
        {
            count -= Time.deltaTime;
            if (count > 0) yield return false;
            else
            {
                // Generate Wall from prefab
                float[] pos = RandomGenYPositions(3);

                GameObject wall = (GameObject)wallQueue.Dequeue();
                wall.transform.position = new Vector3(screenManager.GetCamWidth() / 2, 0, 0);
                wall.SetActive(true);

                wallQueue.Enqueue(wall);

                count += sec;
                yield return true;
            }
        }
    }

    GameObject SpawnWall(float y1, float y2)
    {
        float camWidth = screenManager.GetCamWidth();

        GameObject wall = (GameObject)Instantiate(wallPrefab, new Vector3(camWidth / 2, 0, 0), Quaternion.identity);

        Transform up = wall.transform.Find("UpWall");
        Transform down = wall.transform.Find("DownWall");
        Vector3 upPos = up.position;
        Vector3 downPos = down.position;

        up.position = new Vector3(upPos.x, y1, upPos.z);
        down.position = new Vector3(downPos.x, y2, downPos.z);

        return wall;
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

    public void ResetWalls()
    {
        GameObject wall;

        for (int i = 0; i < WALL_NUM; i++)
        {
            wall = (GameObject)wallQueue.Dequeue();

            wall.transform.position = new Vector3(screenManager.GetCamWidth() / 2, 0, 0);
            wall.SetActive(false);
            wallQueue.Enqueue(wall);
        }
    }
}
