using UnityEngine;
using System.Collections;

public class GenWall : MonoBehaviour {
    public Transform wall;

	// Use this for initialization
	void Start () {
        Instantiate(wall, new Vector3(8.5f, 0, 0), Quaternion.identity);
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
                Instantiate(wall, new Vector3(8.5f, 0, 0), Quaternion.identity);

                count += sec;
                yield return true;
            }
        }
    }
}
