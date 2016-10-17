using UnityEngine;
using System.Collections;

public class PassDetector : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Equals("bird"))
        {
            Player p = Player.GetInstance();
            p.IncScore(1);
            Debug.Log("bird passed: " + other.name);
        }
    }
}
