using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider))]
public class Item : MonoBehaviour, IPoolItem
{
    public enum EItemType
    {
        Health,
        Ammo
    };

    public delegate void ItemPickupEventHandler(Item self);

    public event ItemPickupEventHandler OnPickedup;

    public EItemType ItemType = EItemType.Ammo;
    public int value = 1;


    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            IItemCollector collector = col.gameObject.GetComponent<IItemCollector>();
            if (collector != null)
                collector.Collect(this);

            if (OnPickedup != null)
                OnPickedup(this);

            switch (ItemType)
            {
                case EItemType.Health:
                    GameManager.Instance.healthItemPool.Despawn(transform);
                    break;

                case EItemType.Ammo:
                default:
                    GameManager.Instance.ammoItemPool.Despawn(transform);
                    break;
            }
        }
    }

    public int ID
    {
        get;
        set;
    }
}
