using UnityEngine;
using System.Collections;

public class WaveMessage : MonoBehaviour
{
    public UnityEngine.UI.Text title, subtitle;


    public void Show(int waveNumber, float duration = 3f)
    {
        title.text = "HORDE";
        subtitle.text = "STARTING WAVE " + waveNumber;

        gameObject.SetActive(true);

        Invoke("Hide", duration);
    }

    public void ShowWaveCompleteMessage(int waveNumber, float duration = 3f)
    {
        title.text = "WAVE COMPLETE";
        subtitle.text = "PREPARE FOR WAVE " + (waveNumber + 1).ToString();

        gameObject.SetActive(true);

        // clear any pending hide invoke
        CancelInvoke("Hide");

        Invoke("Hide", duration);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
