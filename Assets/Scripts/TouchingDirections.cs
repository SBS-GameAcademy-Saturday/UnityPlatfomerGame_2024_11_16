using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    // 충돌 접촉에 대한 필터링을 설정하는 파라미터들의 구조체
    public ContactFilter2D contactFilter;
    public float groundHitDistance = 0.05f;
    public float wallHitDistance = 0.2f;

    // 어디에 닿아있는지 확인하는 변수
    // 땅에 닿았는지 확인 여부
    private bool _isGrounded = false;
    // 프로퍼티를 통해서 접근 가능하게 해줄 수 있다.
    public bool isGrounded
    {
        get { return _isGrounded; }
        private set
        {
            _isGrounded = value;
            _animator.SetBool(AnimationStrings.IsGrounded,value);
        }
    }

    // 벽에 닿았는지 확인 여부
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

    // 삼항 연산자 결과 값 = (조건) ? (true 결과 값) : (false 결과 값)
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    private void Awake()
    {
        _touchingCollider = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // 충돌을 체크하는 로직
        isGrounded = _touchingCollider.Cast(Vector2.down, contactFilter, groundHits, groundHitDistance) > 0;
        IsOnWall = _touchingCollider.Cast(wallCheckDirection, contactFilter, wallHits, wallHitDistance) > 0;
    }
}
