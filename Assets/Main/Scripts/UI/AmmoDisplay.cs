using UnityEngine;
using System.Collections;
using UI = UnityEngine.UI;

[RequireComponent(typeof(UI.Text))]
public class AmmoDisplay : MonoBehaviour
{
    private UI.Text _textbox;

    void Start()
    {
        _textbox = GetComponent<UI.Text>();

        GameManager.Instance.Player.EquippedWeapon.OnAmmoChanged += OnPlayerEquippedWeaponAmmoChanged;
    }

    void OnPlayerEquippedWeaponAmmoChanged(Weapon sender, int currentAmmo)
    {
        _textbox.text = "x" + currentAmmo;
    }
}
