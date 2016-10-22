using UnityEngine;
using System.Collections;

public class ScreenManager : MonoBehaviour {
    private static ScreenManager instance;

    public Camera cam;
    private float camWidth, camHeight;

    public static ScreenManager GetInstance()
    {
        if (instance == null)
        {
            instance = (ScreenManager)GameObject.FindObjectOfType<ScreenManager>();
        }
        return instance;
    }

	void Awake()
    {
        cam = Camera.main;
        camHeight = cam.orthographicSize * 2;
        camWidth = camHeight * cam.aspect;
    }

    public float GetCamWidth()
    {
        return camWidth;
    }
    
    public float GetCamHeight()
    {
        return camHeight;
    }
}
