using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class Enemy : Character, ILivingCharacter, IPoolItem
{
    public enum EEnemyType
    {
        Slime,
        Bat,
        Ghost,
        Dragon,
        Skeleton
    }

    public int damage = 1;

    public EEnemyType type = EEnemyType.Slime;

    private SpriteRenderer _renderer;
    private Animator _animator;


    protected override void Start()
    {
 	    base.Start();

        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    public virtual void Reset()
    {
        HP = startingHP;
        if (_renderer)
            _renderer.color = Color.white;
    }

    public void Heal(int amount)
    {
    }

    public void Damage(int amount)
    {
        if (!IsAlive)
            return;

        HP = Mathf.Clamp(HP - amount, 0, 1);
        if (HP == 0)
        {
            Kill();
        }
    }

    public virtual void Kill()
    {
        HP = 0;

        _renderer.color = Color.gray;
        _animator.enabled = false;

        HorderManager.Instance.NotifyEnemyKill();

        GameManager.Instance.Player.Score += scoreValue;

        Invoke("Despawn", 0.5f);
    }

    private void Despawn()
    {
        GameManager.Instance.DespawnEnemy(type, transform);
    }

    void OnTriggerEnter(Collider col)
    {
        if (!IsAlive)
            return;

        if (col.gameObject.CompareTag("Player"))
        {
            ILivingCharacter character = col.gameObject.GetComponent<ILivingCharacter>();
            if (character != null)
                character.Damage(damage);
        }
    }

    protected virtual void Update()
    {
        UpdateOrientation();

        // fail check if enemy falls out of the world
        if (transform.position.y < -10f)
            Kill();
    }


    public bool IsAlive
    {
        get { return HP > 0; }
    }

    public int ID
    {
        get;
        set;
    }
}
