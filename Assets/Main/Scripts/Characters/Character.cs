using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    public enum EFacingDirection
    {
        Left = -1,
        Right = 1,
        Up = 0
    };

    public int startingHP = 5;
    public int maxHP = 10;

    public float gravity = 20f;
    public float runSpeed = 12f;
    public int scoreValue = 10;

    protected CharacterController _characterController;
    protected Vector3 _amountToMove;
    protected float _currentSpeed;


    protected virtual void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    protected virtual void Start()
    {
        HP = startingHP;
    }

    protected void UpdateOrientation()
    {
        if (_amountToMove.x != 0.0f)
            transform.localScale = new Vector2(Mathf.Sign(_amountToMove.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y);
    }

    public int HP
    {
        get;
        protected set;
    }

    public float Direction
    {
        get;
        protected set;
    }

    public EFacingDirection FacingDirection
    {
        get;
        protected set;
    }
}
