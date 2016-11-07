using UnityEngine;
using System.Collections;

public class RestartBtn : MonoBehaviour {
    void OnClick()
    {
        GameManager.GetInstance().Restart();
    }
}
