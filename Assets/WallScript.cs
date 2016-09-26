using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        var pos = transform.position;
        transform.position = new Vector3(pos.x - 0.1f, pos.y, pos.z);
	}
}
