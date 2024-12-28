using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeController : MonoBehaviour
{
    // 이동 속도 값
    [SerializeField] private float moveSpeed;

    // 공격할 수 있는 범위
    [SerializeField] private DetectionZone _biteDetectionZone;

    // 이동 위치에 도달했다고 판단하는 거리 값
    [SerializeField] private float waypointReachedDistance = 0.1f;

    // 이동 위치 리스트
    [SerializeField] private List<Transform> waypoints = new List<Transform>();

    /// <summary>
    /// 멈추는 효과
    /// </summary>
    [SerializeField] private float stopRate = 0.6f;

    /// <summary>
    /// 죽었을 때 사용할 Collider
    /// </summary>
    [SerializeField] private BoxCollider2D _onDeathCollider;

    /// <summary>
    /// 타겟을 가지고 있는지에 대한 여부
    /// </summary>
    private bool _hasTarget = false;

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

    /// <summary>
    /// 애니메이터에 있는 AttackCoolDown 파라미터 변수를 수정하는 프로퍼티
    /// </summary>
    public float AttackCoolDown
    {
        get { return _animator.GetFloat(AnimationStrings.AttackCoolDown); }
        set { _animator.SetFloat(AnimationStrings.AttackCoolDown, value); }
    }

    public bool CanMove => _animator.GetBool(AnimationStrings.CanMove);
    public bool IsAlive => _animator.GetBool(AnimationStrings.IsAlive);

    Rigidbody2D _rigidbody2;
    Animator _animator;
    Damagable _damagable;

    // 내가 현재 가야할 위치
    Transform nextWayPoint;

    // 내가 현재 가야할 위치 인덱스
    private int waypointIndex = 0;

    private void Awake()
    {
        _rigidbody2 = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _damagable = GetComponent<Damagable>();
    }

    void Start()
    {
        nextWayPoint = waypoints[waypointIndex];
        // deathCollider를 꺼준다.
        _onDeathCollider.enabled = false;
    }

    void Update()
    {
        if (!IsAlive) return;

        HasTarget = _biteDetectionZone.detectionColiders.Count > 0;

        // 쿨다운이 0보다 클 경우
        if (AttackCoolDown > 0)
        {
            // 쿨다운 시간을 감소시킨다.
            AttackCoolDown -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (IsAlive)
        {
            if (CanMove) Flight();// 이동할 수 있으면 날라다닌다.
            else _rigidbody2.velocity = Vector2.zero; // 이동 할 수 없으면 velocity를 0,0으로 설정한다.
        }
    }

    /// <summary>
    /// 현재 position을 기준으로 nextWayPoint로 이동을 한다.
    /// nextWayPoint에 다다르면 다음 위치(waypointIndex + 1)로 이동한다.
    /// </summary>
    private void Flight()
    {
        // 이동해야할 방향 vector
        Vector2 directionToWaypoint = (nextWayPoint.position - transform.position).normalized;

        // 해당 방향으로 이동
        _rigidbody2.velocity = directionToWaypoint * moveSpeed;
        // 현재 이동하는 방향으로 회전
        UpdateDirection();

        // 현재 목표 위치와 현재 나의 위치의 거리값을 기준으로 도착했는지 아닌지에 대한 판단을 한다.
        // 현재 목표 위치와 현재 나의 위치의 거리값
        float distance = Vector2.Distance(nextWayPoint.position, transform.position);
        // 도착했는지 아닌지에 대한 판단
        if (distance < waypointReachedDistance)
        {
            // 도착했으면 다음 위치로 넘어갈 수 있도록 
            // index값을 증가한다.
            waypointIndex++;

            // 만약 증가한 waypointIndex가 waypoints의 수 이상이면 0으로 초기화한다.
            if (waypointIndex >= waypoints.Count) waypointIndex = 0;

            // 다음 목표 위치를 갱신한다.
            nextWayPoint = waypoints[waypointIndex];
        }
    }

    // 이동하는 방향을 기준으로 회전한다.
    private void UpdateDirection() 
    {
        // 현재 오른쪽을 바라보는지
        if(transform.localScale.x > 0)
        {
            // 이동하는 방향이 왼쪽인지
            if(_rigidbody2.velocity.x < 0)
            {
                // 방향을 바꿔준다.
                transform.localScale = 
                    new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
        // 현재 왼쪽을 바라보는 지
        else
        {
            // 이동하는 방향이 오른쪽인지
            if (_rigidbody2.velocity.x > 0)
            {
                // 방향을 바꿔준다.
                transform.localScale = 
                    new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
    }

    /// <summary>
    /// 몬스터가 죽었을 때 호출되는 메서드
    /// </summary>
    public void OnDeath()
    {
        // 중력 값을 2로 변경한다.
        _rigidbody2.gravityScale = 2f;
        // 중력에 따라서 떨어질 수 있도록 velocity 값을 변경한다.
        _rigidbody2.velocity = new Vector2(0, _rigidbody2.velocity.y);
        // deathCollider를 켜준다.
        _onDeathCollider.enabled = true;
    }
}
