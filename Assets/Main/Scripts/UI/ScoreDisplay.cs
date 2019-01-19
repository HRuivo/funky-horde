using UnityEngine;
using System.Collections;

public class ScoreDisplay : MonoBehaviour
{
    private UnityEngine.UI.Text text;

    void Start()
    {
        text = GetComponent<UnityEngine.UI.Text>();

        GameManager.Instance.Player.OnScoreChanged += OnPlayerScoreChanged;
    }

    void OnPlayerScoreChanged(Player sender, int newScore)
    {
        text.text = "Score:" + newScore;
    }
}
