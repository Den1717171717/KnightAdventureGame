using System;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(EnemyAI))]
public class EnemyEntity : MonoBehaviour
{
    [SerializeField] private EnemySO enemySO;

    public event EventHandler OnTakeHit;
    public event EventHandler OnDeath;
    
    private int _currentHealth;

    private PolygonCollider2D _polygonCollider2D;
    private BoxCollider2D _boxCollider2D;
    private EnemyAI _enemyAI;
    private bool _didHitPlayerThisSwing;
    

    private void Awake()
    {
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _enemyAI = GetComponent<EnemyAI>();
    }

    private void Start()
    {
        _currentHealth = enemySO.enemyHealth;
    }

    private void OnEnable()
    {
        if (_enemyAI != null) _enemyAI.OnEnemyAttack += EnemyAI_OnEnemyAttack;
    }

    private void OnDisable()
    {
        if (_enemyAI != null) _enemyAI.OnEnemyAttack -= EnemyAI_OnEnemyAttack;
    }

    private void EnemyAI_OnEnemyAttack(object sender, EventArgs e)
    {
        _didHitPlayerThisSwing = false;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_polygonCollider2D.enabled) return;           
        if (_didHitPlayerThisSwing) return;              

        if (collision.TryGetComponent(out Player player))
        {
            player.TakeDamage(transform, enemySO.enemyDamageAmount);
            _didHitPlayerThisSwing = true;
        }
    }


    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        OnTakeHit?.Invoke(this, EventArgs.Empty);
        DetectDeath();
    }

    public void PolygonColliderTurnOff()
    {
        _polygonCollider2D.enabled = false;
    }

    public void PolygonColliderTurnOn()
    {
        _polygonCollider2D.enabled = true;
    }

    private void DetectDeath()
    {
        if (_currentHealth <= 0)
        {
            _boxCollider2D.enabled = false;
            _polygonCollider2D.enabled = false;

            _enemyAI.SetDeathState();

            OnDeath?.Invoke(this, EventArgs.Empty);
        }
    }


}
