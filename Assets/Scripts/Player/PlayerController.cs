using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// 걷는 속도
    /// </summary>
    [SerializeField] private float walkSpeed = 5f;

    /// <summary>
    /// 뛰는 속도
    /// </summary>
    [SerializeField] private float runSpeed = 8.0f;

    /// <summary>
    /// 점프 크기
    /// </summary>
    [SerializeField] private float jumpImpulse = 8f;

    /// <summary>
    /// 각각의 상태에 따른 현재 속도값
    /// </summary>
    public float CurrentMoveSpeed
    {
        get
        {
            // 내가 이동하는 상태인지 
            if (!IsMoving) return 0;
            // 내가 움직을 수 있는 상태인지
            if(!CanMove) return 0;
            // 벽에 닿아있는 상태인지
            if(_touchingDirections.IsOnWall) return 0;
            // 내가 달리는 상태인지
            if(IsRunning) return runSpeed;
            // 달리고 있지 않다
            return walkSpeed;
        }
    }

    private bool _isMoving = false;

    public bool IsMoving
    {
        get { return _isMoving; }
        set
        {
            _isMoving = value;
                    
            // 입력받는 Value가 Vector(0,0)이 아닐시에는 내가 움직이고 있는 상태
            // 입력받는 Value가 Vector(0,0)이 시에는 내가 움직이고 있지 않는 상태
            _animator.SetBool(AnimationStrings.IsMoving, value);
        }
    }
    
    private bool _isRunning = false;

    public bool IsRunning
    {
        get { return _isRunning; }
        set
        {
            _isRunning = value;
            _animator.SetBool(AnimationStrings.IsRunning,value);
        }
    }
    
    public bool CanMove => _animator.GetBool(AnimationStrings.CanMove);

    
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private TouchingDirections _touchingDirections;
    private Vector2 moveInput = Vector2.zero;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _touchingDirections = GetComponent<TouchingDirections>();
    }

    private void FixedUpdate()
    {
        // 차라리 내가 이미 IsRunning에 따라서 계산된 Float 값으로 속력값을 제어하면 되지 않을까?
        // => 어떤 방법이 좋을까?
        // => 프로퍼티
        
        _rigidbody2D.velocity = new Vector2(moveInput.x * CurrentMoveSpeed , _rigidbody2D.velocity.y);
        _animator.SetFloat(AnimationStrings.yVelocity, _rigidbody2D.velocity.y);
    }
    
    public void OnMoveInputAction(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        // 현재 내가 이동하는 상태인지를 체크합니다.
        IsMoving = (moveInput != Vector2.zero);
        
        SetFacingDirection(moveInput);
    }

    /// <summary>
    /// 입력값의 X값이 0보다 크면 오른쪽으로 0보다 작으면 왼쪽을 바라보게
    /// Scale을 조정하는 함수
    /// </summary>
    /// <param name="input"></param>
    public void SetFacingDirection(Vector2 input)
    {
        // x == 0
        if (input.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(input.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    
    
    /// <summary>
    /// 버튼을 처음 눌른 순간에는
    /// context.started     : true
    /// context.performed   : false
    /// context.canceled    : false
    ///
    /// 버튼을 처음 눌른 다음 프레임에는 
    /// context.started     : false
    /// context.performed   : true
    /// context.canceled    : false
    /// 이후에는 값이 안들어온다.
    /// 
    /// 버튼을 뗀 순간에는
    /// context.started     : false
    /// context.performed   : false
    /// context.canceled    : true
    /// </summary>
    /// <param name="context"></param>
    public void OnDashInputAction(InputAction.CallbackContext context)
    {
        // (context.started가 true라면 대쉬 시작 => 쉬프트 키를 누른 순간
        if (context.started)
        {
            IsRunning = true;
        }
        // (context.canceled가 true라면 대쉬 종료 => 쉬프트 키를 뗀 순간
        if (context.canceled)
        {
            IsRunning = false;
        }
    }

    public void OnJumpInputAction(InputAction.CallbackContext context)
    {
        if (context.started && _touchingDirections.isGrounded && CanMove)
        {
            // 위로 올라가는 속력 자체를 제어 => Y축 값을 제어
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpImpulse);
            _animator.SetTrigger(AnimationStrings.Jump);
        }
    }

    public void OnAttackInputAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _animator.SetTrigger(AnimationStrings.Attack);
        }
    }
}
