using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour
{
    public WaveMessage waveMessage;
    public GameOverScreen gameOverScreen;

    void Start()
    {
        HorderManager.Instance.OnNewWaveStart += OnNewWaveStart;
        HorderManager.Instance.OnWaveComplete += OnWaveComplete;

        GameManager.Instance.Player.OnKilled += OnPlayerKilled;
    }

    void OnPlayerKilled(Player sender, int newHealth)
    {
        gameOverScreen.Show(HorderManager.Instance.CurrentWave, sender.Score);
    }

    void OnNewWaveStart(int currentWave)
    {
        waveMessage.Show(currentWave);
    }

    void OnWaveComplete(int currentWave)
    {
        waveMessage.ShowWaveCompleteMessage(currentWave);
    }
}
