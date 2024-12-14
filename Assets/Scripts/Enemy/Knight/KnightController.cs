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
    /// 플레이어 캐릭터가 HitBox 범위에 있는 지 확인하는 Component
    /// </summary>
    [SerializeField] private DetectionZone _hitBoxZone;

    /// <summary>
    /// 타겟을 가지고 있는지에 대한 여부
    /// </summary>
    private bool _hasTarget = false;

    /// <summary>
    /// 멈추는 효과
    /// </summary>
    [SerializeField] private float stopRate = 0.6f;

    /// <summary>
    /// 타겟을 가지고 있는지에 대한 프로퍼티
    /// </summary>
    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            _animator.SetBool(AnimationStrings.HasTarget, value);
            // 여기서부터 쿨타임 시작할 수도 있습니다.
        }
    }

    private EWalkableDirection _walkableDirection = EWalkableDirection.Right;

    public EWalkableDirection WalkableDirection
    {
        get { return _walkableDirection; }
        private set
        {
            // 방향 전환이 일어났다.
            if (_walkableDirection != value)
            {
                // 반대 방향을 바라보도록 설정한다.
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
    /// 애니메이터에 있는 AttackCoolDown 파라미터 변수를 수정하는 프로퍼티
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
        // 죽었으면 종료
        if (!IsAlive) return;

        // hitBoxZone에 플레이어 캐릭터가 들어왔으면 
        // detectionColiders.Count가 1 이상이므로 HasTarget = true로 바꿔준다.
        HasTarget = _hitBoxZone.detectionColiders.Count > 0;

        // 쿨다운이 0보다 클 경우
        if(AttackCoolDown > 0)
        {
            // 쿨다운 시간을 감소시킨다.
            AttackCoolDown -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        // 죽었으면 종료
        if (!IsAlive) return;

        // 벽에 닿았다면 방향 전환
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
            Debug.LogError("설정된 방향이 아닙니다.");
        }
    }

    public void OnCliffDetected()
    {
        // 땅에 붙어있을 때만 반대 방향으로 전환한다.
        if (_touchingDirections.isGrounded)
        {
            FlipDirection();
        }
    }

    /// <summary>
    /// Hit 이벤트 발생시 호출할 함수
    /// </summary>
    /// <param name="knockback"></param>
    public void OnHit(Vector2 knockback)
    {
        _rigidbody2D.velocity = new Vector2(knockback.x, _rigidbody2D.velocity.y + knockback.y);

        if (knockback.x > 0 && transform.localScale.x > 0) FlipDirection();
        else if (knockback.x < 0 && transform.localScale.x < 0) FlipDirection();
    }
}
