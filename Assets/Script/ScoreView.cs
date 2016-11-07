using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreView : MonoBehaviour {
    private static ScoreView instance;

    private Text textUI;
    public string staticText;

    public static ScoreView GetInstance()
    {
        if (instance == null)
        {
            instance = (ScoreView)GameObject.FindObjectOfType(typeof(ScoreView));
        }
        return instance;
    }

    void Awake()
    {
        textUI = GetComponent<Text>();
    }
	
    public void SetScore(int score)
    {
        if (textUI == null) textUI = GetComponent<Text>();
        textUI.text = staticText + score;
    }
}
