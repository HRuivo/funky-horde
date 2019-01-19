using UnityEngine;
using System.Collections;

public class ItemSpawner : Spawner
{
    public Item.EItemType type = Item.EItemType.Ammo;


    void Start()
    {
        Invoke("Spawn", firstSpawnDelay);
    }

    protected override void Spawn()
    {
        Transform itemTrans = null;
        switch (type)
        {
            case Item.EItemType.Health:
                itemTrans = GameManager.Instance.healthItemPool.Spawn();
                break;

            case Item.EItemType.Ammo:
            default:
                itemTrans = GameManager.Instance.ammoItemPool.Spawn();
                break;
        }

        if (itemTrans)
        {
            Item newItem = itemTrans.GetComponent<Item>();
            newItem.transform.position = transform.position;
            newItem.OnPickedup += newItem_OnPickedup;
        }
    }

    void newItem_OnPickedup(Item self)
    {
        self.OnPickedup -= newItem_OnPickedup;

        Invoke("Spawn", Random.Range(minSpawnInterval, maxSpawnInterval));
    }
}
