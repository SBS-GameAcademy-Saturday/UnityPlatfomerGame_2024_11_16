using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{
    // KnockBack �̺�Ʈ
    public UnityEvent<Vector2> _onKnockBack;

    // ü�� ��ȭ �̺�Ʈ
    public UnityEvent<int, int> _onHealthChanged;

    // ĳ���Ͱ� �׾��� �� �̺�Ʈ
    public UnityEvent _onDeath;  

    // ü�� ��
    [SerializeField] private int _health = 100;
    // �ִ� ü�� ��
    [SerializeField] private int _maxHealth = 100;
    // ����ִ����� ���� ���� ����
    [SerializeField] private bool _isAlive = true;
    // ���� ���� ����
    [SerializeField] private bool _isInvincible = false;
    // ���� �ð� 
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

    // ������ �ǰ����� �ð�
    private float timeSinceHit = 0;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // ���� �ð� ó��
    private void Update()
    {
        // ���� ���¶��
        if (_isInvincible)
        {
            // �ǰ� ���Ŀ� �ð��� ���� �ð����� ũ��
            if(timeSinceHit > _invincibleTime)
            {
                // ���� ���¸� �����ϰ�
                _isInvincible = false;
                // �ǰ� ���� �ð��� 0���� �ʱ�ȭ�Ѵ�.
                timeSinceHit = 0;
                return;
            }
            // �ǰ� ���� �ð��� Time.deltaTime��ŭ ��� �����ش�.
            timeSinceHit += Time.deltaTime;
            return;
        }
    }

    // �˹� ȿ��

    // ������ ó��
    // ������ �ߴµ� ���� �ð�, �̹� ����� ĳ���� => ������ �� ���ߴ�
    public bool GetHit(int damage, Vector2 knockback)
    {
        if (IsAlive && !_isInvincible)
        {
            _health -= damage;
            // ���� ���·� ��ȯ
            _isInvincible = true;

            // Velocity �缳���� ������ �Ѵ�.
            _animator.SetBool(AnimationStrings.LockVelocity, true);

            // Hit Ʈ���� 
            _animator.SetTrigger(AnimationStrings.Hit);

            // GUIManager Delegate ȣ��
            GUIManager.characterDamaged.Invoke(transform.position, damage);

            // knockback �̺�Ʈ�� ȣ���Ѵ�.
            _onKnockBack.Invoke(knockback);
            _onHealthChanged.Invoke(_health, _maxHealth);


            // ü���� 0���ϸ� ���� ���·� ��ȯ
            if (_health <= 0) IsAlive = false;
            return true;
        }
        return false;
    }

    // �� ó��
    public bool Heal(int healthRestore) 
    {
        // ��� ������, ���� ü���� �ִ� ä�º��� ���� ��쿡�� 
        // ü���� ȸ����Ų��.
        if(IsAlive && _health < _maxHealth)
        {
            // healthRestore = 20
            // _health = 70
            // _maxHealth = 100

            // _health = 100

            // �ִ�� ȸ���� �� �ִ� ��
            int maxHeal = Mathf.Max(_maxHealth - _health,0);
            // ������ ȸ���� �� �ִ� ��
            int actualHeal = Mathf.Min(maxHeal, healthRestore);

            _health += actualHeal;

            // GUIManager Delegate ȣ��
            GUIManager.characterHealed.Invoke(transform.position, actualHeal);

            _onHealthChanged.Invoke(_health, _maxHealth);
            return true;
            //_health = Mathf.Min(_maxHealth, _health + healthRestore);
        }
        return false;
    }

}
