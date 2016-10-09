using UnityEngine;
using System.Collections;

public class WallCollider : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Equals("bird"))
        {
            Debug.Log("collision detected: " + other.name);
        }
    }
}
