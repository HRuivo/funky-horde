using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    private UnityEngine.UI.Image _healthBarImage;
    [SerializeField]
    private Sprite[] healthStages;

    void Start()
    {
        _healthBarImage = GetComponent<UnityEngine.UI.Image>();

        GameManager.Instance.Player.OnHealthChanged += OnPlayerHealthChanged;
        UpdateHealthBar(GameManager.Instance.Player.HP);
    }

    void OnPlayerHealthChanged(Player sender, int newHealth)
    {
        UpdateHealthBar(newHealth);
    }

    void UpdateHealthBar(int newStage)
    {
        int currentHealthStage = Mathf.Clamp(newStage, 0, healthStages.Length - 1);
        _healthBarImage.sprite = healthStages[currentHealthStage];
    }
}
