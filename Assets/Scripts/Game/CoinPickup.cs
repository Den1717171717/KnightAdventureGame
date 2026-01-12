using UnityEngine;

/// <summary>
/// Simple pickup that adds coins when the player touches it.
/// Create a prefab with a SpriteRenderer + CircleCollider2D (isTrigger = true)
/// and attach this script.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class CoinPickup : MonoBehaviour
{
    [SerializeField] private int value = 1;
    [SerializeField] private float lifeTime = 10f;

    private void Awake()
    {
        // Auto cleanup so dropped coins don't live forever.
        if (lifeTime > 0f)
        {
            Destroy(gameObject, lifeTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (CurrencyManager.Instance == null) return;

        if (other.TryGetComponent(out Player _))
        {
            CurrencyManager.Instance.AddCoins(value);
            Destroy(gameObject);
        }
    }
}
