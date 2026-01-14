using System;
using UnityEngine;

public class EnemyHpDropper : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnemyEntity enemyEntity;

    [Header("Drop")]
    [SerializeField] private GameObject hpPickupPrefab;
    [SerializeField, Range(0f, 1f)] private float dropChance = 0.12f;
    [SerializeField] private Vector2 randomOffset = new Vector2(0.2f, 0.2f);

    private bool _dropped;

    private void Awake()
    {
        if (enemyEntity == null) enemyEntity = GetComponentInChildren<EnemyEntity>();
    }

    private void OnEnable()
    {
        if (enemyEntity != null)
            enemyEntity.OnDeath += OnDeath;
    }

    private void OnDisable()
    {
        if (enemyEntity != null)
            enemyEntity.OnDeath -= OnDeath;
    }

    private void OnDeath(object sender, EventArgs e)
    {
        if (_dropped) return;
        _dropped = true;

        if (hpPickupPrefab == null) return;

        if (UnityEngine.Random.value <= dropChance)
        {
            Vector3 pos = transform.position + new Vector3(
                UnityEngine.Random.Range(-randomOffset.x, randomOffset.x),
                UnityEngine.Random.Range(-randomOffset.y, randomOffset.y),
                0f
            );

            Instantiate(hpPickupPrefab, pos, Quaternion.identity);
        }
    }
}