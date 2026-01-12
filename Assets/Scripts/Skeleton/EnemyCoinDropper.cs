using UnityEngine;

/// <summary>
/// Drops coin pickups when the enemy dies.
/// Attach to the enemy prefab (e.g. Skeleton) and assign a CoinPickup prefab.
/// </summary>
[RequireComponent(typeof(EnemyEntity))]
public class EnemyCoinDropper : MonoBehaviour
{
    [SerializeField] private GameObject coinPickupPrefab;
    [Header("Drop amount")]
    [SerializeField] private int minCoins = 1;
    [SerializeField] private int maxCoins = 3;
    [Header("Scatter")]
    [SerializeField] private float scatterRadius = 0.25f;

    private EnemyEntity _enemyEntity;

    private void Awake()
    {
        _enemyEntity = GetComponent<EnemyEntity>();
    }

    private void OnEnable()
    {
        _enemyEntity.OnDeath += EnemyEntity_OnDeath;
    }

    private void OnDisable()
    {
        _enemyEntity.OnDeath -= EnemyEntity_OnDeath;
    }

    private void EnemyEntity_OnDeath(object sender, System.EventArgs e)
    {
        if (coinPickupPrefab == null) return;

        int count = Random.Range(minCoins, maxCoins + 1);
        for (int i = 0; i < count; i++)
        {
            Vector2 offset = Random.insideUnitCircle * scatterRadius;
            Instantiate(coinPickupPrefab, (Vector2)transform.position + offset, Quaternion.identity);
        }
    }
}
