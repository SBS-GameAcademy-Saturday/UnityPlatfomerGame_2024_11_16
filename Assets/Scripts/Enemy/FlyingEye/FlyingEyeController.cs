using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeController : MonoBehaviour
{
    // �̵� �ӵ� ��
    [SerializeField] private float moveSpeed;

    // ������ �� �ִ� ����
    [SerializeField] private DetectionZone _biteDetectionZone;

    // �̵� ��ġ�� �����ߴٰ� �Ǵ��ϴ� �Ÿ� ��
    [SerializeField] private float waypointReachedDistance = 0.1f;

    // �̵� ��ġ ����Ʈ
    [SerializeField] private List<Transform> waypoints = new List<Transform>();

    /// <summary>
    /// ���ߴ� ȿ��
    /// </summary>
    [SerializeField] private float stopRate = 0.6f;

    /// <summary>
    /// �׾��� �� ����� Collider
    /// </summary>
    [SerializeField] private BoxCollider2D _onDeathCollider;

    /// <summary>
    /// Ÿ���� ������ �ִ����� ���� ����
    /// </summary>
    private bool _hasTarget = false;

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

    /// <summary>
    /// �ִϸ����Ϳ� �ִ� AttackCoolDown �Ķ���� ������ �����ϴ� ������Ƽ
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

    // ���� ���� ������ ��ġ
    Transform nextWayPoint;

    // ���� ���� ������ ��ġ �ε���
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
        // deathCollider�� ���ش�.
        _onDeathCollider.enabled = false;
    }

    void Update()
    {
        if (!IsAlive) return;

        HasTarget = _biteDetectionZone.detectionColiders.Count > 0;

        // ��ٿ��� 0���� Ŭ ���
        if (AttackCoolDown > 0)
        {
            // ��ٿ� �ð��� ���ҽ�Ų��.
            AttackCoolDown -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (IsAlive)
        {
            if (CanMove) Flight();// �̵��� �� ������ ����ٴѴ�.
            else _rigidbody2.velocity = Vector2.zero; // �̵� �� �� ������ velocity�� 0,0���� �����Ѵ�.
        }
    }

    /// <summary>
    /// ���� position�� �������� nextWayPoint�� �̵��� �Ѵ�.
    /// nextWayPoint�� �ٴٸ��� ���� ��ġ(waypointIndex + 1)�� �̵��Ѵ�.
    /// </summary>
    private void Flight()
    {
        // �̵��ؾ��� ���� vector
        Vector2 directionToWaypoint = (nextWayPoint.position - transform.position).normalized;

        // �ش� �������� �̵�
        _rigidbody2.velocity = directionToWaypoint * moveSpeed;
        // ���� �̵��ϴ� �������� ȸ��
        UpdateDirection();

        // ���� ��ǥ ��ġ�� ���� ���� ��ġ�� �Ÿ����� �������� �����ߴ��� �ƴ����� ���� �Ǵ��� �Ѵ�.
        // ���� ��ǥ ��ġ�� ���� ���� ��ġ�� �Ÿ���
        float distance = Vector2.Distance(nextWayPoint.position, transform.position);
        // �����ߴ��� �ƴ����� ���� �Ǵ�
        if (distance < waypointReachedDistance)
        {
            // ���������� ���� ��ġ�� �Ѿ �� �ֵ��� 
            // index���� �����Ѵ�.
            waypointIndex++;

            // ���� ������ waypointIndex�� waypoints�� �� �̻��̸� 0���� �ʱ�ȭ�Ѵ�.
            if (waypointIndex >= waypoints.Count) waypointIndex = 0;

            // ���� ��ǥ ��ġ�� �����Ѵ�.
            nextWayPoint = waypoints[waypointIndex];
        }
    }

    // �̵��ϴ� ������ �������� ȸ���Ѵ�.
    private void UpdateDirection() 
    {
        // ���� �������� �ٶ󺸴���
        if(transform.localScale.x > 0)
        {
            // �̵��ϴ� ������ ��������
            if(_rigidbody2.velocity.x < 0)
            {
                // ������ �ٲ��ش�.
                transform.localScale = 
                    new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
        // ���� ������ �ٶ󺸴� ��
        else
        {
            // �̵��ϴ� ������ ����������
            if (_rigidbody2.velocity.x > 0)
            {
                // ������ �ٲ��ش�.
                transform.localScale = 
                    new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
    }

    /// <summary>
    /// ���Ͱ� �׾��� �� ȣ��Ǵ� �޼���
    /// </summary>
    public void OnDeath()
    {
        // �߷� ���� 2�� �����Ѵ�.
        _rigidbody2.gravityScale = 2f;
        // �߷¿� ���� ������ �� �ֵ��� velocity ���� �����Ѵ�.
        _rigidbody2.velocity = new Vector2(0, _rigidbody2.velocity.y);
        // deathCollider�� ���ش�.
        _onDeathCollider.enabled = true;
    }
}
