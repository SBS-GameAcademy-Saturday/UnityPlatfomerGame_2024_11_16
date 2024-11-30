using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    // �浹 ���˿� ���� ���͸��� �����ϴ� �Ķ���͵��� ����ü
    public ContactFilter2D contactFilter;
    public float groundHitDistance = 0.05f;
    public float wallHitDistance = 0.2f;

    // ��� ����ִ��� Ȯ���ϴ� ����
    // ���� ��Ҵ��� Ȯ�� ����
    private bool _isGrounded = false;
    // ������Ƽ�� ���ؼ� ���� �����ϰ� ���� �� �ִ�.
    public bool isGrounded
    {
        get { return _isGrounded; }
        private set
        {
            _isGrounded = value;
            _animator.SetBool(AnimationStrings.IsGrounded,value);
        }
    }

    // ���� ��Ҵ��� Ȯ�� ����
    private bool _isOnWall = false;

    public bool IsOnWall
    {
        get { return _isOnWall; }
        private set
        {
            _isOnWall = value;
        }
    }

    CapsuleCollider2D _touchingCollider;
    Animator _animator;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];

    // ���� ������ ��� �� = (����) ? (true ��� ��) : (false ��� ��)
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    private void Awake()
    {
        _touchingCollider = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // �浹�� üũ�ϴ� ����
        isGrounded = _touchingCollider.Cast(Vector2.down, contactFilter, groundHits, groundHitDistance) > 0;
        IsOnWall = _touchingCollider.Cast(wallCheckDirection, contactFilter, wallHits, wallHitDistance) > 0;
    }
}
