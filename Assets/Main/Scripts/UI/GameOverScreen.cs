using UnityEngine;
using System.Collections;
using UI = UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public UI.Text txtWave, txtScore;

    public string formattedWaveMessage = "YOU MANAGED TO REACH <color=#ffa500ff>WAVE #{0}</color>";
    public string formattedScoreMessage = "Score:<color=#ffa500ff>{0}</color>";

    public void Show(int waveReached, int score)
    {
        gameObject.SetActive(true);

        txtWave.text = string.Format(formattedWaveMessage, waveReached);
        txtScore.text = string.Format(formattedScoreMessage, score);
    }
}
