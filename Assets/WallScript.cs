using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour {
    private IEnumerator fadeOut;

    // Use this for initialization
    void Start () {
        fadeOut = FadeOutIn(5000);
    }

    // Update is called once per frame
    void Update () {
        if (fadeOut.MoveNext() != false)
        {
            MoveLeft();
        }
    }

    void MoveLeft()
    {
        var pos = transform.position;
        transform.position = new Vector3(pos.x - 0.05f, pos.y, pos.z);
    }
    
    IEnumerator FadeOutIn(long millis)
    {
        float countdown = millis / 1000.0f;

        while ((countdown -= Time.deltaTime) > 0) yield return false;

        gameObject.SetActive(false);
        yield return true;
    }
}