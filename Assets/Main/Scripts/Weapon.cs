using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public int maxAmmo = 10;
    public int startingAmmo = 5;
    public int damage = 10;

    public delegate void WeaponEventHandler(Weapon sender, int currentAmmo);

    public event WeaponEventHandler OnAmmoChanged;


    void Start()
    {
        SetAmmo(startingAmmo);
    }

    public bool Fire()
    {
        if (HasAmmo)
        {
            SetAmmo(Ammo - 1);
            SpawnProjectile(Owner.FacingDirection);

            return true;
        }

        return false;
    }

    private void SpawnProjectile(Character.EFacingDirection direction)
    {
        Transform projTrans = GameManager.Instance.projectilePool.Spawn();

        if (projTrans)
        {
            Projectile proj = projTrans.GetComponent<Projectile>();
            proj.transform.position = Owner.transform.position;
            proj.DamageAuthor = Owner.gameObject;
            proj.damage = damage;
            proj.SetDirection(direction);
        }
    }

    private void SetAmmo(int ammo)
    {
        Ammo = ammo;

        if (OnAmmoChanged != null)
            OnAmmoChanged(this, Ammo);
    }

    public void GiveAmmo(int amount)
    {
        SetAmmo(Ammo + amount);
    }

    public bool HasAmmo
    {
        get { return Ammo > 0; }
    }

    public Character Owner
    {
        get;
        set;
    }

    public int Ammo
    {
        get;
        private set;
    }
}
