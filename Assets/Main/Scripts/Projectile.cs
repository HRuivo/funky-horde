using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour, IPoolItem
{
    public float speed = 5f;
    public int damage = 1;


    public void SetDirection(Character.EFacingDirection direction)
    {
        float angle = 0f;

        switch (direction)
        {
            case Character.EFacingDirection.Left:
                angle = 180f;
                break;

            case Character.EFacingDirection.Right:
                angle = 0f;
                break;

            case Character.EFacingDirection.Up:
                angle = 90f;
                break;
        }

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void FixedUpdate()
    {
        transform.Translate(speed * Time.fixedDeltaTime, 0f, 0f);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject != DamageAuthor)
        {
            ILivingCharacter character = col.gameObject.GetComponent<ILivingCharacter>();
            if (character != null)
                character.Damage(damage);

            GameManager.Instance.projectilePool.Despawn(transform);
        }
    }

    public GameObject DamageAuthor
    {
        get;
        set;
    }

    public int ID
    {
        get;
        set;
    }
}
