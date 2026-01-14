using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private bool spawningEnabled = true;

    private Vector2 _intervalRange = new Vector2(5f, 20f);
    private float _timer;

    private void Start()
    {
        ResetTimer();
    }

    private void Update()
    {
        if (!spawningEnabled) return;
        if (enemyPrefab == null) return;

        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            Spawn();
            ResetTimer();
        }
    }

    public void SetIntervalRange(Vector2 range)
    {
        _intervalRange = range;
        _timer = Mathf.Min(_timer, Random.Range(_intervalRange.x, _intervalRange.y));
    }

    public void SetSpawningEnabled(bool enabled)
    {
        spawningEnabled = enabled;
        if (spawningEnabled && _timer <= 0f) ResetTimer();
    }

    private void ResetTimer()
    {
        _timer = Random.Range(_intervalRange.x, _intervalRange.y);
    }

    private void Spawn()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}