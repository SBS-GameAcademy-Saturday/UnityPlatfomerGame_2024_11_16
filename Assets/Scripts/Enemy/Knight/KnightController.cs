using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public enum EWalkableDirection
{
    Right,
    Left,
}

public class KnightController : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 3.0f;

    /// <summary>
    /// �÷��̾� ĳ���Ͱ� HitBox ������ �ִ� �� Ȯ���ϴ� Component
    /// </summary>
    [SerializeField] private DetectionZone _hitBoxZone;

    /// <summary>
    /// Ÿ���� ������ �ִ����� ���� ����
    /// </summary>
    private bool _hasTarget = false;

    /// <summary>
    /// ���ߴ� ȿ��
    /// </summary>
    [SerializeField] private float stopRate = 0.6f;

    /// <summary>
    /// Ÿ���� ������ �ִ����� ���� ������Ƽ
    /// </summary>
    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            _animator.SetBool(AnimationStrings.HasTarget, value);
            // ���⼭���� ��Ÿ�� ������ ���� �ֽ��ϴ�.
        }
    }

    private EWalkableDirection _walkableDirection = EWalkableDirection.Right;

    public EWalkableDirection WalkableDirection
    {
        get { return _walkableDirection; }
        private set
        {
            // ���� ��ȯ�� �Ͼ��.
            if (_walkableDirection != value)
            {
                // �ݴ� ������ �ٶ󺸵��� �����Ѵ�.
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);

                if (value == EWalkableDirection.Right)
                {
                    moveDirection = Vector2.right;
                }
                else if (value == EWalkableDirection.Left)
                {
                    moveDirection = Vector2.left;
                }
            }
            _walkableDirection = value;
        }
    }

    /// <summary>
    /// �ִϸ����Ϳ� �ִ� AttackCoolDown �Ķ���� ������ �����ϴ� ������Ƽ
    /// </summary>
    public float AttackCoolDown
    {
        get { return _animator.GetFloat(AnimationStrings.AttackCoolDown); }
        set {_animator.SetFloat(AnimationStrings.AttackCoolDown, value); }
    }

    public bool CanMove => _animator.GetBool(AnimationStrings.CanMove);
    public bool IsAlive => _animator.GetBool(AnimationStrings.IsAlive);

    private Rigidbody2D _rigidbody2D;
    private TouchingDirections _touchingDirections;
    private Animator _animator;
    private Vector2 moveDirection = Vector2.right;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _touchingDirections = GetComponent<TouchingDirections>();
    }

    private void Update()
    {
        // �׾����� ����
        if (!IsAlive) return;

        // hitBoxZone�� �÷��̾� ĳ���Ͱ� �������� 
        // detectionColiders.Count�� 1 �̻��̹Ƿ� HasTarget = true�� �ٲ��ش�.
        HasTarget = _hitBoxZone.detectionColiders.Count > 0;

        // ��ٿ��� 0���� Ŭ ���
        if(AttackCoolDown > 0)
        {
            // ��ٿ� �ð��� ���ҽ�Ų��.
            AttackCoolDown -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        // �׾����� ����
        if (!IsAlive) return;

        // ���� ��Ҵٸ� ���� ��ȯ
        if (_touchingDirections.IsOnWall)
        {
            FlipDirection();
        }

        if (!_animator.GetBool(AnimationStrings.LockVelocity))
        {
            if (CanMove)
                _rigidbody2D.velocity = new Vector2(moveDirection.x * walkSpeed, _rigidbody2D.velocity.y);
            else
                _rigidbody2D.velocity = 
                    new Vector2(Mathf.Lerp(_rigidbody2D.velocity.x,0, stopRate), 
                    _rigidbody2D.velocity.y);
        }
    }

    private void FlipDirection()
    {
        if(WalkableDirection == EWalkableDirection.Right)
        {
            WalkableDirection = EWalkableDirection.Left;
        }
        else if(WalkableDirection == EWalkableDirection.Left)
        {
            WalkableDirection = EWalkableDirection.Right;
        }
        else
        {
            Debug.LogError("������ ������ �ƴմϴ�.");
        }
    }

    public void OnCliffDetected()
    {
        // ���� �پ����� ���� �ݴ� �������� ��ȯ�Ѵ�.
        if (_touchingDirections.isGrounded)
        {
            FlipDirection();
        }
    }

    /// <summary>
    /// Hit �̺�Ʈ �߻��� ȣ���� �Լ�
    /// </summary>
    /// <param name="knockback"></param>
    public void OnHit(Vector2 knockback)
    {
        _rigidbody2D.velocity = new Vector2(knockback.x, _rigidbody2D.velocity.y + knockback.y);

        if (knockback.x > 0 && transform.localScale.x > 0) FlipDirection();
        else if (knockback.x < 0 && transform.localScale.x < 0) FlipDirection();
    }
}
