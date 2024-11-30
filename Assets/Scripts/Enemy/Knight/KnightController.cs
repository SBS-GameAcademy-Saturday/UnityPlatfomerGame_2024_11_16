using System.Collections;
using System.Collections.Generic;
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

    private Rigidbody2D _rigidbody2D;
    private TouchingDirections _touchingDirections;

    private Vector2 moveDirection = Vector2.right;
    private EWalkableDirection _walkableDirection = EWalkableDirection.Right;

    public EWalkableDirection WalkableDirection
    {
        get { return _walkableDirection; }
        private set
        {
            // ���� ��ȯ�� �Ͼ��.
            if(_walkableDirection != value)
            {
                // �ݴ� ������ �ٶ󺸵��� �����Ѵ�.
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);

                if(value == EWalkableDirection.Right)
                {
                    moveDirection = Vector2.right;
                }
                else if(value == EWalkableDirection.Left)
                {
                    moveDirection = Vector2.left;
                }
            }
            _walkableDirection = value;
        }
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _touchingDirections = GetComponent<TouchingDirections>();
    }

    private void FixedUpdate()
    {
        // ���� ��Ҵٸ� ���� ��ȯ
        if (_touchingDirections.IsOnWall)
        {
            FlipDirection();
        }

        _rigidbody2D.velocity = new Vector2(moveDirection.x * walkSpeed, _rigidbody2D.velocity.y);
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
}
