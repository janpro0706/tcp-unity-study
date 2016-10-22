using UnityEngine;
using System.Collections;

public class PassDetector : MonoBehaviour {
    GameManager gameManager;
    Player player;

    void Awake()
    {
        transform.position = new Vector3(-7.0f, 6.5f, 0);
        gameManager = GameManager.GetInstance();
        player = Player.GetInstance();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("Trigger Exit " + other.gameObject.tag);
        if (other.gameObject.tag.Equals("Wall"))
        {
            if (player.IsAlive())
            {
                gameManager.IncScore(1);
            }
        }
    }
}
