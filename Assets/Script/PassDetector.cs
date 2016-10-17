using UnityEngine;
using System.Collections;

public class PassDetector : MonoBehaviour {
    void Awake()
    {
        transform.position = new Vector3(-7.0f, 0, 0);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Trigger Exit " + other.gameObject.tag);
        if (other.gameObject.tag.Equals("Wall"))
        {
            Player p = Player.GetInstance();
            p.IncScore(1);
        }
    }
}
