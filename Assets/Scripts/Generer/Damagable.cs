using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{
    // KnockBack 이벤트
    public UnityEvent<Vector2> _onKnockBack;

    // 체력 변화 이벤트
    public UnityEvent<int, int> _onHealthChanged;

    // 캐릭터가 죽었을 때 이벤트
    public UnityEvent _onDeath;  

    // 체력 값
    [SerializeField] private int _health = 100;
    // 최대 체력 값
    [SerializeField] private int _maxHealth = 100;
    // 살아있는지에 대한 상태 변수
    [SerializeField] private bool _isAlive = true;
    // 무적 상태 변수
    [SerializeField] private bool _isInvincible = false;
    // 무적 시간 
    [SerializeField] private float _invincibleTime = 0.25f;

    public bool IsAlive
    {
        get { return _isAlive; }
        set 
        { 
            _isAlive = value;
            if (!_isAlive) _onDeath.Invoke();
            _animator.SetBool(AnimationStrings.IsAlive, value);
        }
    }

    public int Health => _health;
    public int MaxHealth => _maxHealth;

    private Animator _animator;

    // 데미지 피격이후 시간
    private float timeSinceHit = 0;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // 무적 시간 처리
    private void Update()
    {
        // 무적 상태라면
        if (_isInvincible)
        {
            // 피격 이후에 시간이 무적 시간보다 크면
            if(timeSinceHit > _invincibleTime)
            {
                // 무적 상태를 해제하고
                _isInvincible = false;
                // 피격 이후 시간을 0으로 초기화한다.
                timeSinceHit = 0;
                return;
            }
            // 피격 이후 시간을 Time.deltaTime만큼 계속 더해준다.
            timeSinceHit += Time.deltaTime;
            return;
        }
    }

    // 넉백 효과

    // 데미지 처리
    // 공격을 했는데 무적 시간, 이미 사망한 캐릭터 => 공격이 안 통했다
    public bool GetHit(int damage, Vector2 knockback)
    {
        if (IsAlive && !_isInvincible)
        {
            _health -= damage;
            // 무적 상태로 전환
            _isInvincible = true;

            // Velocity 재설정을 막도록 한다.
            _animator.SetBool(AnimationStrings.LockVelocity, true);

            // Hit 트리거 
            _animator.SetTrigger(AnimationStrings.Hit);

            // GUIManager Delegate 호출
            GUIManager.characterDamaged.Invoke(transform.position, damage);

            // knockback 이벤트를 호출한다.
            _onKnockBack.Invoke(knockback);
            _onHealthChanged.Invoke(_health, _maxHealth);


            // 체력이 0이하면 죽은 상태로 전환
            if (_health <= 0) IsAlive = false;
            return true;
        }
        return false;
    }

    // 힐 처리
    public bool Heal(int healthRestore) 
    {
        // 살아 있을때, 현재 체력이 최대 채력보다 작을 경우에만 
        // 체력을 회복시킨다.
        if(IsAlive && _health < _maxHealth)
        {
            // healthRestore = 20
            // _health = 70
            // _maxHealth = 100

            // _health = 100

            // 최대로 회복할 수 있는 양
            int maxHeal = Mathf.Max(_maxHealth - _health,0);
            // 실제로 회복할 수 있는 양
            int actualHeal = Mathf.Min(maxHeal, healthRestore);

            _health += actualHeal;

            // GUIManager Delegate 호출
            GUIManager.characterHealed.Invoke(transform.position, actualHeal);

            _onHealthChanged.Invoke(_health, _maxHealth);
            return true;
            //_health = Mathf.Min(_maxHealth, _health + healthRestore);
        }
        return false;
    }

}
