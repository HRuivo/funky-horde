using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Weapon))]
public class Player : Character, ILivingCharacter, IItemCollector
{
    public Transform body;

    public float acceleration = 30f;
    public float jumpHeight = 12;
    public int jumpsAllowed = 2;
    public float invulnerabilityDuration = 1.0f;

    private SpriteRenderer _renderer;
    private Animator _animator;
    private int _totalJumps;
    private bool _canTakeDamage = true;
    private EFacingDirection _lastHorizontalDirection = EFacingDirection.Right;

    private int _score = 0;

    public delegate void PlayerHealthEventHandler(Player sender, int newHealth);
    public delegate void PlayerScoreEventHandler(Player sender, int newScore);

    public event PlayerHealthEventHandler OnHealthChanged;
    public event PlayerHealthEventHandler OnKilled;

    public event PlayerScoreEventHandler OnScoreChanged;


    protected override void Awake()
    {
        base.Awake();

        _animator = GetComponentInChildren<Animator>();
        _renderer = GetComponentInChildren<SpriteRenderer>();

        EquippedWeapon = GetComponent<Weapon>();
        EquippedWeapon.Owner = this;

        Direction = 1f;
    }

    protected override void Start()
    {
        base.Start();

        if (OnHealthChanged != null)
            OnHealthChanged(this, HP);
    }

    void Update()
    {
        if (!IsAlive)
            return;

        if ((_characterController.collisionFlags & CollisionFlags.Sides) != 0)
        {
            _currentSpeed = 0;
        }

        if ((_characterController.collisionFlags & CollisionFlags.Below) != 0)
        {
            _amountToMove.y = -1f;
            _totalJumps = 0;

            _animator.SetBool("Is Grounded", true);
        }
        else
        {
            _amountToMove.y -= gravity * Time.deltaTime;
            _animator.SetBool("Is Grounded", false);
        }

        if (Input.GetButtonDown("Jump") && _totalJumps < jumpsAllowed)
        {
            Jump();
        }

        float targetSpeed = Input.GetAxisRaw("Horizontal") * runSpeed;
        _currentSpeed = IncrementTowards(_currentSpeed, targetSpeed, acceleration);
        
        if (transform.position.z != 0)
        {
            _amountToMove.z = -transform.position.z;
        }

        _amountToMove.x = _currentSpeed;
        
        //if (_amountToMove.x != 0)
        //    body.localScale = new Vector2(Mathf.Sign(_amountToMove.x) * Mathf.Abs(body.localScale.x), body.localScale.y);

        UpdateOrientation();

        if (_amountToMove.x != 0.0f)
            Direction = Mathf.Sign(_amountToMove.x);

        _characterController.Move(_amountToMove * Time.deltaTime);

        _animator.SetBool("Is Moving", _amountToMove.x != 0.0f);

        if (_amountToMove.x != 0)
        {
            FacingDirection = (EFacingDirection)((int)Mathf.Sign(_amountToMove.x));
            _lastHorizontalDirection = FacingDirection;
        }
        if (Input.GetAxisRaw("Vertical") > 0f)
        {
            FacingDirection = EFacingDirection.Up;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            FireWeapon();
        }
    }

    void LateUpdate()
    {
        FacingDirection = _lastHorizontalDirection;
    }

    // Increase n towards target by speed
    private float IncrementTowards(float n, float target, float a)
    {
        if (n == target)
        {
            return n;
        }
        else
        {
            float dir = Mathf.Sign(target - n);
            n += a * Time.deltaTime * dir;
            return (dir == Mathf.Sign(target - n)) ? n : target;
        }
    }

    void Jump()
    {
        _totalJumps++;
        _amountToMove.y = jumpHeight;

        _animator.SetTrigger("Jumped");
    }

    void FireWeapon()
    {
        if (EquippedWeapon.Fire())
            _animator.SetTrigger("Fired");
    }

    public void Heal(int amount)
    {
        HP = Mathf.Clamp(HP + amount, 0, maxHP);

        if (OnHealthChanged != null)
            OnHealthChanged(this, HP);
    }

    public void Damage(int amount)
    {
        if (!_canTakeDamage)
            return;

        _canTakeDamage = false;

        HP = Mathf.Clamp(HP - amount, 0, maxHP);

        if (OnHealthChanged != null)
            OnHealthChanged(this, HP);

        if (!IsAlive)
        {
            Kill();
        }
        else
        {
            _renderer.color = Color.gray;
            Invoke("ResetDamage", invulnerabilityDuration);
        }
    }

    void ResetDamage()
    {
        _canTakeDamage = true;
        _renderer.color = Color.white;
    }

    public void Kill()
    {
        HP = 0;

        if (OnKilled != null)
            OnKilled(this, 0);

        _animator.SetBool("Is Dead", true);
    }

    public void Collect(Item item)
    {
        switch (item.ItemType)
        {
            case Item.EItemType.Health:
                Heal(item.value);
                break;

            case Item.EItemType.Ammo:
                EquippedWeapon.GiveAmmo(item.value);
                break;
        }
    }

    
    public bool IsAlive
    {
        get { return HP > 0; }
    }

    public Weapon EquippedWeapon
    {
        get;
        private set;
    }

    public int Score
    {
        get { return _score; }
        set
        {
            _score = value;
            if (OnScoreChanged != null)
                OnScoreChanged(this, _score);
        }
    }
}
